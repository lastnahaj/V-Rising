using System.Collections.Concurrent;
using BepInEx.Logging;
using Discord;
using Discord.WebSocket;
using SanguineRelay.Core;
using SanguineRelay.Game;
using SanguineRelay.Persistence;
using SanguineRelay.State;

namespace SanguineRelay.Discord;

internal sealed class DiscordService : IAsyncDisposable
{
    private readonly RelayOptions _options;
    private readonly ServerStateService _state;
    private readonly GameIntegrationService _game;
    private readonly GameThreadDispatcher _dispatcher;
    private readonly ManualLogSource _log;
    private readonly DiscordSocketClient _client;
    private readonly DiscordOutboundQueue _outbound;
    private readonly DiscordPermissionService _permissions;
    private readonly DiscordStatusService _status;
    private readonly AuditHealth _auditHealth = new();
    private readonly ConcurrentDictionary<ulong, long> _chatCooldowns = new();
    private readonly ConcurrentDictionary<ulong, CancellationTokenSource> _pendingLeaves = new();
    private readonly CancellationTokenSource _shutdown = new();
    private int _statusStarted;
    private int _commandRegistrationStarted;
    private bool _disposed;

    public DiscordService(
        RelayOptions options,
        ServerStateService state,
        GameIntegrationService game,
        GameThreadDispatcher dispatcher,
        PersistenceStore persistence,
        ManualLogSource log)
    {
        _options = options;
        _state = state;
        _game = game;
        _dispatcher = dispatcher;
        _log = log;
        _outbound = new DiscordOutboundQueue(log);
        _client = new DiscordSocketClient(new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.Guilds | GatewayIntents.GuildMessages | GatewayIntents.MessageContent,
            AlwaysDownloadUsers = false,
            LogGatewayIntentWarnings = true,
            MessageCacheSize = 20,
            DefaultRetryMode = RetryMode.RetryRatelimit,
            DefaultRatelimitCallback = info =>
            {
                _outbound.ObserveRateLimit(info.RetryAfter ?? 1);
                return Task.CompletedTask;
            }
        });
        _permissions = new DiscordPermissionService(options);
        _status = new DiscordStatusService(options, state, _client, _outbound, persistence, log);

        _client.Log += HandleLogAsync;
        _client.Ready += HandleReadyAsync;
        _client.Connected += HandleConnectedAsync;
        _client.Disconnected += HandleDisconnectedAsync;
        _client.MessageReceived += HandleMessageAsync;
        _client.SlashCommandExecuted += HandleSlashCommandAsync;

        _game.GameChatReceived += HandleGameChat;
        _game.PlayerJoined += HandlePlayerJoined;
        _game.PlayerLeft += HandlePlayerLeft;
        _game.PlayerDied += HandlePlayerDeath;
        _game.PvpKill += HandlePvpKill;
        _game.VBloodKill += HandleVBloodKill;
        _game.CastleBreached += HandleCastleBreach;
        _game.ServerOnline += HandleServerOnline;
    }

    public bool IsConnected => _client.ConnectionState == ConnectionState.Connected;

    public async Task StartAsync()
    {
        if (!_options.Discord.Enabled || string.IsNullOrWhiteSpace(_options.Discord.Token) || _options.Discord.GuildId == 0)
        {
            _log.LogWarning("Discord integration is disabled because its required configuration is incomplete.");
            return;
        }

        try
        {
            await _client.LoginAsync(TokenType.Bot, _options.Discord.Token.Trim()).ConfigureAwait(false);
            await _client.StartAsync().ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            _log.LogError($"Discord startup failed; the game server will continue without Discord. {exception.GetType().Name}: {Redact(exception.Message)}");
        }
    }

    public async Task NotifyStoppingAsync()
    {
        _state.SetServerOffline();
        if (!IsConnected)
        {
            return;
        }

        await _status.StopAsync().ConfigureAwait(false);
        _status.ScheduleFinalOffline(_state.Current);
        if (_options.Events.ServerLifecycle)
        {
            QueueMessage(_options.Discord.ServerEventsChannelId, "🔴 V Rising server is offline.", "server offline event");
        }

        try
        {
            await _outbound.FlushAsync(TimeSpan.FromSeconds(8)).ConfigureAwait(false);
        }
        catch (Exception exception) when (exception is OperationCanceledException or InvalidOperationException)
        {
            _log.LogWarning("Final Discord offline updates did not flush before the bounded shutdown deadline.");
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        _shutdown.Cancel();
        foreach (var leave in _pendingLeaves.Values)
        {
            leave.Cancel();
            leave.Dispose();
        }

        _pendingLeaves.Clear();
        _game.GameChatReceived -= HandleGameChat;
        _game.PlayerJoined -= HandlePlayerJoined;
        _game.PlayerLeft -= HandlePlayerLeft;
        _game.PlayerDied -= HandlePlayerDeath;
        _game.PvpKill -= HandlePvpKill;
        _game.VBloodKill -= HandleVBloodKill;
        _game.CastleBreached -= HandleCastleBreach;
        _game.ServerOnline -= HandleServerOnline;

        await _status.DisposeAsync().ConfigureAwait(false);
        await _outbound.DisposeAsync().ConfigureAwait(false);
        try
        {
            if (_client.ConnectionState != ConnectionState.Disconnected)
            {
                await _client.StopAsync().ConfigureAwait(false);
            }

            await _client.LogoutAsync().ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            _log.LogWarning($"Discord shutdown reported {exception.GetType().Name}: {Redact(exception.Message)}");
        }

        _client.Dispose();
        _shutdown.Dispose();
    }

    private Task HandleReadyAsync()
    {
        if (Interlocked.CompareExchange(ref _commandRegistrationStarted, 1, 0) == 0)
        {
            _ = RegisterCommandsAsync();
        }
        if (Interlocked.Exchange(ref _statusStarted, 1) == 0)
        {
            _status.Start();
        }

        if (_options.Events.ServerLifecycle)
        {
            QueueMessage(_options.Discord.ServerEventsChannelId, "✅ Discord integration connected.", "Discord connected event");
            if (_state.Current.IsOnline)
            {
                QueueMessage(_options.Discord.ServerEventsChannelId, "✅ V Rising server is online.", "server online event");
            }
        }

        _log.LogInfo("Discord connected.");
        return Task.CompletedTask;
    }

    private Task HandleConnectedAsync()
    {
        _log.LogInfo("Discord gateway connected.");
        return Task.CompletedTask;
    }

    private Task HandleDisconnectedAsync(Exception exception)
    {
        var detail = exception is null ? "No detail was supplied." : $"{exception.GetType().Name}: {Redact(exception.Message)}";
        _log.LogWarning($"Discord gateway disconnected; automatic reconnect remains active. {detail}");
        return Task.CompletedTask;
    }

    private Task HandleLogAsync(LogMessage message)
    {
        var text = Redact(message.Message ?? message.Exception?.Message ?? string.Empty);
        switch (message.Severity)
        {
            case LogSeverity.Critical:
            case LogSeverity.Error:
                _log.LogError($"Discord: {text}");
                break;
            case LogSeverity.Warning:
                _log.LogWarning($"Discord: {text}");
                break;
            case LogSeverity.Debug:
            case LogSeverity.Verbose:
                if (_options.General.DebugLogging)
                {
                    _log.LogDebug($"Discord: {text}");
                }

                break;
            default:
                if (!string.IsNullOrWhiteSpace(text))
                {
                    _log.LogInfo($"Discord: {text}");
                }

                break;
        }

        return Task.CompletedTask;
    }

    private async Task HandleMessageAsync(SocketMessage socketMessage)
    {
        if (!_options.Chat.DiscordToGame || socketMessage is not SocketUserMessage message ||
            message.Channel.Id != _options.Discord.ChatChannelId)
        {
            return;
        }

        var context = new DiscordInboundMessageContext(
            message.Author.Id,
            message.Author.IsBot,
            _client.CurrentUser is not null && message.Author.Id == _client.CurrentUser.Id,
            message.Source == MessageSource.Webhook,
            message.Source == MessageSource.System);
        if (DiscordInboundPolicy.ShouldIgnore(
                context,
                _options.Chat.IgnoreDiscordBots,
                _options.Chat.IgnoredUserIds,
                _options.Chat.IgnoredBotIds))
        {
            return;
        }

        var now = Environment.TickCount64;
        var last = _chatCooldowns.GetOrAdd(message.Author.Id, long.MinValue / 2);
        if (now - last < _options.Chat.CooldownMilliseconds)
        {
            return;
        }

        _chatCooldowns[message.Author.Id] = now;
        var body = TextSanitizer.ForGame(message.Content, _options.Chat.MaxDiscordMessageLength);
        if (string.IsNullOrWhiteSpace(body))
        {
            return;
        }

        var displayName = message.Author is SocketGuildUser guildUser ? guildUser.DisplayName : message.Author.Username;
        var values = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase)
        {
            ["prefix"] = TextSanitizer.ForGame(_options.Chat.Prefix, 40),
            ["user"] = TextSanitizer.GameDisplayName(displayName),
            ["message"] = body
        };
        var rendered = TextSanitizer.ForGame(TemplateFormatter.Format(_options.Chat.Format, values), 500);

        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(_shutdown.Token);
        timeout.CancelAfter(TimeSpan.FromSeconds(5));
        try
        {
            await _dispatcher.InvokeAsync(() => _game.SendSystemMessage(rendered), timeout.Token).ConfigureAwait(false);
        }
        catch (OperationCanceledException)
        {
            _log.LogWarning("A Discord chat message could not reach the game thread before its timeout.");
        }
        catch (Exception exception)
        {
            _log.LogError($"Discord-to-game chat failed: {exception.GetType().Name}: {exception.Message}");
        }
    }

    private async Task HandleSlashCommandAsync(SocketSlashCommand command)
    {
        try
        {
            if (command.GuildId != _options.Discord.GuildId)
            {
                await command.RespondAsync("This command is not available in this server.", ephemeral: true).ConfigureAwait(false);
                return;
            }

            switch (command.Data.Name)
            {
                case "status":
                    await command.RespondAsync(embed: DiscordEmbedFactory.BuildCommandStatus(_state.Current)).ConfigureAwait(false);
                    break;
                case "players":
                    await command.RespondAsync(embed: DiscordEmbedFactory.BuildPlayers(_state.Current)).ConfigureAwait(false);
                    break;
                case "player":
                    await HandlePlayerCommandAsync(command).ConfigureAwait(false);
                    break;
                case "announce":
                    await HandleAnnounceCommandAsync(command).ConfigureAwait(false);
                    break;
                case "relay-status":
                    await HandleRelayStatusCommandAsync(command).ConfigureAwait(false);
                    break;
                default:
                    await command.RespondAsync("Unknown command.", ephemeral: true).ConfigureAwait(false);
                    break;
            }
        }
        catch (Exception exception)
        {
            _log.LogError($"Slash command '{command.Data.Name}' failed: {exception.GetType().Name}: {exception.Message}");
            if (DiscordAuditPolicy.RequiresAudit(command.Data.Name))
            {
                Audit(command, AuditOutcome.ExecutionFailure, exception.GetType().Name);
            }

            if (!command.HasResponded)
            {
                await command.RespondAsync("The command could not be completed.", ephemeral: true).ConfigureAwait(false);
            }
        }
    }

    private async Task HandlePlayerCommandAsync(SocketSlashCommand command)
    {
        if (!_permissions.CanExecute(command, RelayPermission.ViewPlayer))
        {
            await command.RespondAsync("You do not have permission to use this command.", ephemeral: true).ConfigureAwait(false);
            Audit(command, AuditOutcome.Denied);
            return;
        }

        var name = command.Data.Options.FirstOrDefault()?.Value?.ToString() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(name))
        {
            await command.RespondAsync("A player name is required.", ephemeral: true).ConfigureAwait(false);
            Audit(command, AuditOutcome.ValidationFailure, "missing player name");
            return;
        }

        var player = _state.FindPlayer(name);
        if (player is null)
        {
            await command.RespondAsync("No online player matched that exact name.", ephemeral: true).ConfigureAwait(false);
            Audit(command, AuditOutcome.ValidationFailure, "player not found");
            return;
        }

        var builder = new EmbedBuilder()
            .WithTitle(TextSanitizer.DiscordDisplay(player.Name, 256))
            .AddField("Online", "Yes", true);
        if (!string.IsNullOrWhiteSpace(player.Clan))
        {
            builder.AddField("Clan", TextSanitizer.DiscordDisplay(player.Clan, 1024), true);
        }

        if (player.GearLevel.HasValue)
        {
            builder.AddField("Gear level", player.GearLevel.Value, true);
        }

        await command.RespondAsync(embed: builder.Build(), ephemeral: true).ConfigureAwait(false);
        Audit(command, AuditOutcome.Success);
    }

    private async Task HandleAnnounceCommandAsync(SocketSlashCommand command)
    {
        if (!_permissions.CanExecute(command, RelayPermission.Announce))
        {
            await command.RespondAsync("You do not have permission to use this command.", ephemeral: true).ConfigureAwait(false);
            Audit(command, AuditOutcome.Denied);
            return;
        }

        var input = command.Data.Options.FirstOrDefault()?.Value?.ToString() ?? string.Empty;
        var message = TextSanitizer.ForGame(input, 400);
        if (string.IsNullOrWhiteSpace(message))
        {
            await command.RespondAsync("Announcement text is required.", ephemeral: true).ConfigureAwait(false);
            Audit(command, AuditOutcome.ValidationFailure, "missing announcement text");
            return;
        }

        var rendered = TextSanitizer.ForGame($"{_options.Chat.AnnouncementPrefix} {message}", 500);
        await command.DeferAsync(ephemeral: true).ConfigureAwait(false);
        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(_shutdown.Token);
        timeout.CancelAfter(TimeSpan.FromSeconds(5));
        try
        {
            await _dispatcher.InvokeAsync(() => _game.SendSystemMessage(rendered), timeout.Token).ConfigureAwait(false);
            await command.ModifyOriginalResponseAsync(properties => properties.Content = "Announcement sent.").ConfigureAwait(false);
            Audit(command, AuditOutcome.Success);
        }
        catch (Exception exception) when (exception is not OperationCanceledException)
        {
            await command.ModifyOriginalResponseAsync(properties => properties.Content = "The game server rejected the announcement.").ConfigureAwait(false);
            Audit(command, AuditOutcome.ExecutionFailure, exception.GetType().Name);
        }
        catch (OperationCanceledException)
        {
            await command.ModifyOriginalResponseAsync(properties => properties.Content = "The game server did not process the announcement in time.").ConfigureAwait(false);
            Audit(command, AuditOutcome.Timeout);
        }
    }

    private async Task HandleRelayStatusCommandAsync(SocketSlashCommand command)
    {
        if (!_permissions.CanExecute(command, RelayPermission.Administer))
        {
            await command.RespondAsync("You do not have permission to use this command.", ephemeral: true).ConfigureAwait(false);
            Audit(command, AuditOutcome.Denied);
            return;
        }

        var snapshot = _state.Current;
        var dispatcher = _dispatcher.Metrics;
        var outbound = _outbound.Metrics;
        var audit = _auditHealth.Metrics;
        var text = $"SanguineRelay {PluginInfo.Version}\n\n" +
                   $"Discord: {(IsConnected ? "Connected" : "Disconnected")}\n" +
                   $"Game state: {(_game.IsInitialized ? "Active" : "Waiting for server world")}\n" +
                   $"Players: {snapshot.OnlinePlayers}/{snapshot.MaximumPlayers}\n" +
                   $"Presence: {(_options.Presence.Enabled ? "Enabled" : "Disabled")}\n" +
                   $"Status embed: {(_options.StatusEmbed.Enabled ? "Enabled" : "Disabled")}\n" +
                   $"Voice counter: {(_options.VoiceCounter.Enabled ? "Enabled" : "Disabled")}\n" +
                   "RCON: Unsupported\n" +
                   $"Uptime: {DiscordEmbedFactory.FormatDuration(snapshot.Uptime)}\n" +
                   $"Game queue: {dispatcher.Pending} pending, {dispatcher.Queued} queued, {dispatcher.Rejected} rejected\n" +
                   $"Discord queue: {outbound.Depth} queued, {outbound.Rejected} rejected, {outbound.Retries} retries, {outbound.PermanentFailures} failed\n" +
                   $"Audit: {audit.Queued} queued, {audit.Dropped} dropped, {audit.DeliveryFailures} delivery failures";
        await command.RespondAsync(text, ephemeral: true).ConfigureAwait(false);
        Audit(command, AuditOutcome.Success);
    }

    private async Task RegisterCommandsAsync()
    {
        try
        {
            var guild = _client.GetGuild(_options.Discord.GuildId);
            if (guild is null)
            {
                Interlocked.Exchange(ref _commandRegistrationStarted, 0);
                _log.LogError("The configured Discord guild is unavailable. Check GuildId and bot membership.");
                return;
            }

            var commands = new SlashCommandProperties[]
            {
                new SlashCommandBuilder().WithName("status").WithDescription("Show V Rising server status.").Build(),
                new SlashCommandBuilder().WithName("players").WithDescription("List online V Rising players.").Build(),
                new SlashCommandBuilder()
                    .WithName("player")
                    .WithDescription("Show details for an online player.")
                    .AddOption("name", ApplicationCommandOptionType.String, "Exact character name.", isRequired: true)
                    .Build(),
                new SlashCommandBuilder()
                    .WithName("announce")
                    .WithDescription("Send an announcement to the V Rising server.")
                    .AddOption("message", ApplicationCommandOptionType.String, "Announcement text.", isRequired: true, minLength: 1, maxLength: 400)
                    .Build(),
                new SlashCommandBuilder().WithName("relay-status").WithDescription("Show SanguineRelay health information.").Build()
            };

            var existing = await guild.GetApplicationCommandsAsync().ConfigureAwait(false);
            foreach (var desired in commands)
            {
                var matches = existing
                    .Where(command => command.Type == ApplicationCommandType.Slash && command.Name == desired.Name.Value)
                    .ToArray();
                if (matches.Length == 0)
                {
                    await guild.CreateApplicationCommandAsync(desired).ConfigureAwait(false);
                    continue;
                }

                await matches[0].ModifyAsync<SlashCommandProperties>(properties =>
                {
                    properties.Name = desired.Name;
                    properties.Description = desired.Description;
                    properties.Options = desired.Options;
                    properties.IsDMEnabled = false;
                }).ConfigureAwait(false);

                foreach (var duplicate in matches.Skip(1))
                {
                    await duplicate.DeleteAsync().ConfigureAwait(false);
                }
            }

            _log.LogInfo("SanguineRelay guild commands reconciled without changing unrelated application commands.");
        }
        catch (Exception exception)
        {
            Interlocked.Exchange(ref _commandRegistrationStarted, 0);
            _log.LogError($"Slash command registration failed: {exception.GetType().Name}: {exception.Message}");
        }
    }

    private void HandleGameChat(GameChatMessage chat)
    {
        if (!_options.Chat.GameToDiscord)
        {
            return;
        }

        var name = TextSanitizer.DiscordDisplay(chat.Player.Name, 80);
        var message = TextSanitizer.DiscordChatContent(chat.Message, _options.Chat.AllowGameMentionsInDiscord, 1800);
        if (!string.IsNullOrWhiteSpace(message))
        {
            QueueMessage(
                _options.Discord.ChatChannelId,
                $"**{name}:** {message}",
                "game chat relay",
                _options.Chat.AllowGameMentionsInDiscord);
        }
    }

    private void HandlePlayerJoined(PlayerSnapshot player)
    {
        if (_pendingLeaves.TryRemove(player.PlatformId, out var pending))
        {
            pending.Cancel();
            pending.Dispose();
            return;
        }

        if (_options.Events.PlayerJoin)
        {
            QueueEvent(_options.Discord.PlayerEventsChannelId, _options.Events.JoinMessage, "player join event", ("player", player.Name));
        }
    }

    private void HandlePlayerLeft(PlayerSnapshot player)
    {
        if (!_options.Events.PlayerLeave)
        {
            return;
        }

        var cancellation = CancellationTokenSource.CreateLinkedTokenSource(_shutdown.Token);
        if (_pendingLeaves.TryGetValue(player.PlatformId, out var old))
        {
            old.Cancel();
            old.Dispose();
        }

        _pendingLeaves[player.PlatformId] = cancellation;
        _ = PublishLeaveAfterDelayAsync(player, cancellation);
    }

    private async Task PublishLeaveAfterDelayAsync(PlayerSnapshot player, CancellationTokenSource cancellation)
    {
        try
        {
            await Task.Delay(TimeSpan.FromSeconds(_options.Events.ReconnectSuppressionSeconds), cancellation.Token).ConfigureAwait(false);
            if (_pendingLeaves.TryRemove(player.PlatformId, out _))
            {
                QueueEvent(_options.Discord.PlayerEventsChannelId, _options.Events.LeaveMessage, "player leave event", ("player", player.Name));
            }
        }
        catch (OperationCanceledException)
        {
            if (_options.General.DebugLogging)
            {
                _log.LogDebug($"Reconnect suppression cancelled the leave event for {player.Name}.");
            }
        }
        finally
        {
            cancellation.Dispose();
        }
    }

    private void HandlePlayerDeath(PlayerDeathEvent death) =>
        QueueEvent(_options.Discord.PlayerDeathChannelId, _options.Events.PlayerDeathMessage, "player death event", ("player", death.Victim.Name));

    private void HandlePvpKill(PvpKillEvent kill) =>
        QueueEvent(_options.Discord.PvpChannelId, _options.Events.PvpKillMessage, "PvP kill event", ("killer", kill.Killer.Name), ("victim", kill.Victim.Name));

    private void HandleVBloodKill(VBloodKillEvent kill)
    {
        var players = string.Join(", ", kill.Players.Select(player => player.Name));
        QueueEvent(_options.Discord.VBloodChannelId, _options.Events.VBloodMessage, "V Blood kill event", ("player", players), ("boss", kill.Boss));
    }

    private void HandleCastleBreach(CastleBreachEvent breach) =>
        QueueEvent(_options.Discord.RaidChannelId, _options.Events.RaidMessage, "castle breach event", ("player", breach.Owner.Name), ("killer", breach.Attacker.Name));

    private void HandleServerOnline()
    {
        if (_options.Events.ServerLifecycle && IsConnected)
        {
            QueueMessage(_options.Discord.ServerEventsChannelId, "✅ V Rising server is online.", "server online event");
        }
    }

    private void QueueEvent(ulong channelId, string template, string description, params (string Key, string Value)[] replacements)
    {
        var values = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);
        foreach (var replacement in replacements)
        {
            values[replacement.Key] = TextSanitizer.DiscordDisplay(replacement.Value, 300);
        }

        QueueMessage(channelId, TemplateFormatter.Format(template, values), description);
    }

    private bool QueueMessage(
        ulong channelId,
        string message,
        string description,
        bool allowMentions = false,
        Action? onPermanentFailure = null)
    {
        if (channelId == 0 || string.IsNullOrWhiteSpace(message))
        {
            return false;
        }

        var safe = TextSanitizer.FinalizeDiscordMessage(message, allowMentions);
        return _outbound.Enqueue(description, async cancellationToken =>
        {
            var channel = _client.GetGuild(_options.Discord.GuildId)?.GetTextChannel(channelId);
            if (channel is null)
            {
                throw new InvalidOperationException($"The configured Discord channel is unavailable for {description}.");
            }

            var requestOptions = new RequestOptions
            {
                CancelToken = cancellationToken,
                RetryMode = RetryMode.RetryRatelimit
            };
            await channel.SendMessageAsync(
                safe,
                allowedMentions: allowMentions ? AllowedMentions.All : AllowedMentions.None,
                options: requestOptions).ConfigureAwait(false);
        }, false, onPermanentFailure);
    }

    private bool Audit(SocketSlashCommand command, AuditOutcome outcome, string? detail = null)
    {
        if (_options.Discord.AdminLogChannelId == 0)
        {
            _auditHealth.RecordDropped();
            return false;
        }

        var arguments = string.Join(' ', command.Data.Options.Select(option => option.Value?.ToString()));
        var displayName = command.User is SocketGuildUser guildUser ? guildUser.DisplayName : command.User.Username;
        var audit = "**ADMIN COMMAND**\n" +
                    $"Discord User: {TextSanitizer.DiscordDisplay(displayName, 80)}\n" +
                    $"Discord User ID: {command.User.Id}\n" +
                    $"Command: /{TextSanitizer.DiscordDisplay(command.Data.Name, 32)} {TextSanitizer.DiscordDisplay(arguments, 500)}\n" +
                    $"Result: {TextSanitizer.DiscordDisplay(DiscordAuditPolicy.Describe(outcome, detail), 200)}\n" +
                    $"Timestamp: {DateTimeOffset.UtcNow:O}";
        var queued = QueueMessage(
            _options.Discord.AdminLogChannelId,
            audit,
            "administrative audit",
            onPermanentFailure: _auditHealth.RecordDeliveryFailure);
        if (queued)
        {
            _auditHealth.RecordQueued();
        }
        else
        {
            _auditHealth.RecordDropped();
        }

        return queued;
    }

    private string Redact(string text)
    {
        if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(_options.Discord.Token))
        {
            return text;
        }

        return text.Replace(_options.Discord.Token, "[REDACTED]", StringComparison.Ordinal);
    }
}
