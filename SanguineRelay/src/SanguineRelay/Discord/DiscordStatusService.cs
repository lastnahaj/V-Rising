using System.Threading.Channels;
using BepInEx.Logging;
using Discord;
using Discord.WebSocket;
using SanguineRelay.Core;
using SanguineRelay.Persistence;
using SanguineRelay.State;

namespace SanguineRelay.Discord;

internal sealed class DiscordStatusService : IAsyncDisposable
{
    private sealed record UpdateRequest(ServerSnapshot Snapshot, StatusUpdateReason Reason);

    private readonly RelayOptions _options;
    private readonly ServerStateService _state;
    private readonly DiscordSocketClient _client;
    private readonly DiscordOutboundQueue _outbound;
    private readonly PersistenceStore _persistence;
    private readonly ManualLogSource _log;
    private readonly Channel<UpdateRequest> _updates = Channel.CreateBounded<UpdateRequest>(new BoundedChannelOptions(1)
    {
        FullMode = BoundedChannelFullMode.DropOldest,
        SingleReader = true,
        SingleWriter = false
    });
    private readonly CancellationTokenSource _shutdown = new();
    private Task? _worker;
    private DateTimeOffset _lastPresenceAttempt;
    private DateTimeOffset _lastEmbedAttempt;
    private DateTimeOffset _lastVoiceAttempt;
    private string? _lastPresence;
    private string? _lastVoiceName;
    private string? _lastEmbedFingerprint;
    private ulong _statusMessageId;
    private bool _voicePermissionsApplied;
    private int _stopped;

    public DiscordStatusService(
        RelayOptions options,
        ServerStateService state,
        DiscordSocketClient client,
        DiscordOutboundQueue outbound,
        PersistenceStore persistence,
        ManualLogSource log)
    {
        _options = options;
        _state = state;
        _client = client;
        _outbound = outbound;
        _persistence = persistence;
        _log = log;
        _statusMessageId = options.StatusEmbed.MessageId != 0
            ? options.StatusEmbed.MessageId
            : persistence.Current.StatusMessageId;
    }

    public void Start()
    {
        if (_worker is not null)
        {
            return;
        }

        _state.Changed += OnStateChanged;
        _worker = Task.Run(RunAsync);
        _updates.Writer.TryWrite(new UpdateRequest(_state.Current, StatusUpdateReason.Initial));
    }

    public int ScheduleFinalOffline(ServerSnapshot snapshot)
    {
        var plan = StatusRefreshPolicy.CreateFinalOfflinePlan(
            _options.Presence.Enabled,
            _options.StatusEmbed.Enabled,
            _options.VoiceCounter.Enabled);
        var scheduled = 0;
        scheduled += plan.Presence && PublishPresence(snapshot, true) ? 1 : 0;
        scheduled += plan.StatusEmbed && PublishEmbed(snapshot, StatusUpdateReason.Shutdown) ? 1 : 0;
        scheduled += plan.VoiceCounter && PublishVoiceCounter(snapshot, true) ? 1 : 0;
        return scheduled;
    }

    public async ValueTask DisposeAsync()
    {
        await StopAsync().ConfigureAwait(false);
        _shutdown.Dispose();
    }

    public async Task StopAsync()
    {
        if (Interlocked.Exchange(ref _stopped, 1) != 0)
        {
            return;
        }

        _state.Changed -= OnStateChanged;
        _updates.Writer.TryComplete();
        _shutdown.Cancel();
        if (_worker is not null)
        {
            try
            {
                await _worker.ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                _log.LogDebug("Discord status publisher stopped.");
            }
        }
    }

    private void OnStateChanged(ServerSnapshot snapshot) =>
        _updates.Writer.TryWrite(new UpdateRequest(snapshot, StatusUpdateReason.StateChanged));

    private async Task RunAsync()
    {
        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(15));
        var timerTask = WaitForTimerAsync(timer, _shutdown.Token);
        var readTask = _updates.Reader.ReadAsync(_shutdown.Token).AsTask();

        while (!_shutdown.IsCancellationRequested)
        {
            var completed = await Task.WhenAny(timerTask, readTask).ConfigureAwait(false);
            UpdateRequest request;
            if (completed == readTask)
            {
                request = await readTask.ConfigureAwait(false);
                await Task.Delay(TimeSpan.FromSeconds(2), _shutdown.Token).ConfigureAwait(false);
                while (_updates.Reader.TryRead(out var newer))
                {
                    request = newer with
                    {
                        Reason = request.Reason == StatusUpdateReason.Initial
                            ? StatusUpdateReason.Initial
                            : newer.Reason
                    };
                }

                readTask = _updates.Reader.ReadAsync(_shutdown.Token).AsTask();
            }
            else
            {
                if (!await timerTask.ConfigureAwait(false))
                {
                    break;
                }

                request = new UpdateRequest(_state.Current, StatusUpdateReason.Interval);
                timerTask = WaitForTimerAsync(timer, _shutdown.Token);
            }

            Publish(request);
        }
    }

    private static Task<bool> WaitForTimerAsync(PeriodicTimer timer, CancellationToken token) =>
        timer.WaitForNextTickAsync(token).AsTask();

    private void Publish(UpdateRequest request)
    {
        var now = DateTimeOffset.UtcNow;
        var force = request.Reason == StatusUpdateReason.Initial;
        if (_options.Presence.Enabled && (force || now - _lastPresenceAttempt >= TimeSpan.FromSeconds(_options.Presence.UpdateIntervalSeconds)))
        {
            _lastPresenceAttempt = now;
            PublishPresence(request.Snapshot, force);
        }

        if (_options.StatusEmbed.Enabled && (force || now - _lastEmbedAttempt >= TimeSpan.FromSeconds(_options.StatusEmbed.UpdateIntervalSeconds)))
        {
            _lastEmbedAttempt = now;
            PublishEmbed(request.Snapshot, request.Reason);
        }

        if (_options.VoiceCounter.Enabled && (force || now - _lastVoiceAttempt >= TimeSpan.FromSeconds(_options.VoiceCounter.UpdateIntervalSeconds)))
        {
            _lastVoiceAttempt = now;
            PublishVoiceCounter(request.Snapshot, force);
        }
    }

    private bool PublishPresence(ServerSnapshot snapshot, bool force)
    {
        if (!_options.Presence.Enabled)
        {
            return false;
        }

        var format = !snapshot.IsOnline
            ? _options.Presence.OfflineFormat
            : snapshot.OnlinePlayers == 0 ? _options.Presence.EmptyFormat : _options.Presence.OnlineFormat;
        var text = TextSanitizer.DiscordPlainText(TemplateFormatter.Format(format, DiscordEmbedFactory.Values(snapshot)), 128);
        var fingerprint = $"{_options.Presence.OnlineStatus}|{_options.Presence.ActivityType}|{text}";
        if (!force && fingerprint == _lastPresence)
        {
            return false;
        }

        return _outbound.Enqueue("update presence", async _ =>
        {
            var status = Enum.TryParse<UserStatus>(_options.Presence.OnlineStatus, true, out var parsedStatus)
                ? parsedStatus
                : UserStatus.Online;
            var activity = Enum.TryParse<ActivityType>(_options.Presence.ActivityType, true, out var parsedActivity)
                ? parsedActivity
                : ActivityType.Watching;
            await _client.SetStatusAsync(status).ConfigureAwait(false);
            await _client.SetGameAsync(text, type: activity).ConfigureAwait(false);
            _lastPresence = fingerprint;
        }, true);
    }

    private bool PublishEmbed(ServerSnapshot snapshot, StatusUpdateReason reason)
    {
        if (!_options.StatusEmbed.Enabled || _options.StatusEmbed.ChannelId == 0)
        {
            return false;
        }

        var fingerprint = string.Join('|',
            snapshot.IsOnline,
            snapshot.ServerName,
            snapshot.PublicAddress,
            snapshot.MaximumPlayers,
            snapshot.DaysRunning,
            string.Join(',', snapshot.Players.Select(player => player.Name)));
        var contentChanged = fingerprint != _lastEmbedFingerprint;
        if (reason == StatusUpdateReason.StateChanged && !contentChanged)
        {
            return false;
        }

        var canCreate = reason != StatusUpdateReason.Shutdown;
        var isIdempotent = _statusMessageId != 0 && !_options.StatusEmbed.AutoRecreateDeletedMessage;
        return _outbound.Enqueue("update status embed", async cancellationToken =>
        {
            var requestOptions = CreateRequestOptions(cancellationToken);
            var guild = _client.GetGuild(_options.Discord.GuildId);
            var channel = guild?.GetTextChannel(_options.StatusEmbed.ChannelId);
            if (channel is null)
            {
                _log.LogWarning("Status embed channel is unavailable; only this feature is disabled until the next refresh.");
                return;
            }

            var messageId = _statusMessageId;
            IUserMessage? message = null;
            if (messageId != 0)
            {
                try
                {
                    message = await channel.GetMessageAsync(messageId, options: requestOptions).ConfigureAwait(false) as IUserMessage;
                }
                catch (global::Discord.Net.HttpException exception) when ((int)exception.HttpCode == 404)
                {
                    if (!_options.StatusEmbed.AutoRecreateDeletedMessage)
                    {
                        _log.LogWarning("The configured status message was deleted and automatic recreation is disabled.");
                        return;
                    }
                }
            }

            if (message is null && messageId != 0 && !_options.StatusEmbed.AutoRecreateDeletedMessage)
            {
                _log.LogWarning("The configured status message is unavailable and automatic recreation is disabled.");
                return;
            }

            if (message is not null && !StatusRefreshPolicy.ShouldModifyExistingEmbed(
                    contentChanged,
                    _options.StatusEmbed.ShowTimestamp,
                    reason))
            {
                return;
            }

            var embed = DiscordEmbedFactory.BuildStatus(snapshot, _options.StatusEmbed);
            if (message is null)
            {
                if (!canCreate || (!_options.StatusEmbed.AutoCreateMessage && messageId == 0))
                {
                    return;
                }

                message = await channel.SendMessageAsync(embed: embed, options: requestOptions).ConfigureAwait(false);
                _statusMessageId = message.Id;
                _persistence.SetStatusMessageId(message.Id);
                _log.LogInfo("Status embed initialized.");
            }
            else
            {
                await message.ModifyAsync(properties => properties.Embed = embed, requestOptions).ConfigureAwait(false);
            }

            _lastEmbedFingerprint = fingerprint;
        }, isIdempotent);
    }

    private bool PublishVoiceCounter(ServerSnapshot snapshot, bool force)
    {
        if (!_options.VoiceCounter.Enabled)
        {
            return false;
        }

        var channelId = _options.VoiceCounter.ChannelId;
        if (channelId == 0)
        {
            return false;
        }

        var format = !snapshot.IsOnline
            ? _options.VoiceCounter.OfflineFormat
            : snapshot.OnlinePlayers == 0 ? _options.VoiceCounter.EmptyFormat : _options.VoiceCounter.Format;
        var name = TextSanitizer.DiscordPlainText(TemplateFormatter.Format(format, DiscordEmbedFactory.Values(snapshot)), 100);
        if (!force && name == _lastVoiceName)
        {
            return false;
        }

        return _outbound.Enqueue("update voice counter", async cancellationToken =>
        {
            var requestOptions = CreateRequestOptions(cancellationToken);
            var guild = _client.GetGuild(_options.Discord.GuildId);
            var channel = guild?.GetVoiceChannel(channelId);
            if (guild is null || channel is null)
            {
                _log.LogWarning("Voice counter channel is unavailable; only this feature is disabled until the next refresh.");
                return;
            }

            if (_options.VoiceCounter.LockChannel && !_voicePermissionsApplied)
            {
                var current = channel.GetPermissionOverwrite(guild.EveryoneRole) ?? OverwritePermissions.InheritAll;
                var permissions = VoicePermissionPolicy.ApplyLock(current);
                if (permissions.AllowValue == current.AllowValue && permissions.DenyValue == current.DenyValue)
                {
                    _voicePermissionsApplied = true;
                }
                else
                {
                    try
                    {
                        await channel.AddPermissionOverwriteAsync(guild.EveryoneRole, permissions, requestOptions).ConfigureAwait(false);
                        _voicePermissionsApplied = true;
                    }
                    catch (Exception exception)
                    {
                        _log.LogWarning($"Voice counter permission lock could not be applied; channel renaming will continue. {exception.GetType().Name}");
                    }
                }
            }

            if (!channel.Name.Equals(name, StringComparison.Ordinal))
            {
                await channel.ModifyAsync(properties => properties.Name = name, requestOptions).ConfigureAwait(false);
            }

            _lastVoiceName = name;
        }, true);
    }

    private static RequestOptions CreateRequestOptions(CancellationToken cancellationToken) => new()
    {
        CancelToken = cancellationToken,
        RetryMode = RetryMode.RetryRatelimit
    };
}

internal enum StatusUpdateReason
{
    Initial,
    StateChanged,
    Interval,
    Shutdown
}

internal static class StatusRefreshPolicy
{
    public static bool ShouldModifyExistingEmbed(
        bool contentChanged,
        bool showTimestamp,
        StatusUpdateReason reason) =>
        contentChanged ||
        reason is StatusUpdateReason.Initial or StatusUpdateReason.Shutdown ||
        showTimestamp && reason == StatusUpdateReason.Interval;

    public static FinalOfflinePlan CreateFinalOfflinePlan(bool presence, bool statusEmbed, bool voiceCounter) =>
        new(presence, statusEmbed, voiceCounter);
}

internal readonly record struct FinalOfflinePlan(bool Presence, bool StatusEmbed, bool VoiceCounter);

internal static class VoicePermissionPolicy
{
    public static OverwritePermissions ApplyLock(OverwritePermissions current) => current.Modify(
        viewChannel: PermValue.Allow,
        connect: PermValue.Deny,
        speak: PermValue.Deny);
}
