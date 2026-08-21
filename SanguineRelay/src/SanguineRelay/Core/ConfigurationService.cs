using BepInEx.Configuration;
using BepInEx.Logging;

namespace SanguineRelay.Core;

internal sealed record RelayOptions(
    GeneralOptions General,
    DiscordOptions Discord,
    ChatOptions Chat,
    EventOptions Events,
    PresenceOptions Presence,
    VoiceCounterOptions VoiceCounter,
    StatusEmbedOptions StatusEmbed,
    ServerInfoOptions ServerInfo,
    PermissionOptions Permissions);

internal sealed record GeneralOptions(bool Enabled, bool DebugLogging);

internal sealed record DiscordOptions(
    bool Enabled,
    string Token,
    ulong GuildId,
    ulong ChatChannelId,
    ulong PlayerEventsChannelId,
    ulong PlayerDeathChannelId,
    ulong PvpChannelId,
    ulong VBloodChannelId,
    ulong RaidChannelId,
    ulong ServerEventsChannelId,
    ulong AdminLogChannelId);

internal sealed record ChatOptions(
    bool GameToDiscord,
    bool DiscordToGame,
    bool RelayGlobal,
    bool RelayLocal,
    bool RelayClan,
    string Prefix,
    string Format,
    string AnnouncementPrefix,
    int MaxDiscordMessageLength,
    int CooldownMilliseconds,
    bool IgnoreDiscordBots,
    IReadOnlySet<ulong> IgnoredUserIds,
    IReadOnlySet<ulong> IgnoredBotIds,
    bool AllowGameMentionsInDiscord);

internal sealed record EventOptions(
    bool PlayerJoin,
    bool PlayerLeave,
    bool PlayerDeaths,
    bool PvpKills,
    bool VBloodKills,
    bool CastleRaids,
    bool ServerLifecycle,
    int ReconnectSuppressionSeconds,
    string JoinMessage,
    string LeaveMessage,
    string PlayerDeathMessage,
    string PvpKillMessage,
    string VBloodMessage,
    string RaidMessage);

internal sealed record PresenceOptions(
    bool Enabled,
    string OnlineStatus,
    string ActivityType,
    string OnlineFormat,
    string EmptyFormat,
    string OfflineFormat,
    int UpdateIntervalSeconds);

internal sealed record VoiceCounterOptions(
    bool Enabled,
    ulong ChannelId,
    string Format,
    string EmptyFormat,
    string OfflineFormat,
    int UpdateIntervalSeconds,
    bool LockChannel);

internal sealed record StatusEmbedOptions(
    bool Enabled,
    ulong ChannelId,
    ulong MessageId,
    bool AutoCreateMessage,
    bool AutoRecreateDeletedMessage,
    string TitleFormat,
    bool ShowIpPort,
    string IpPortLabel,
    string OnlineCountLabel,
    string DaysRunningLabel,
    string PlayersSectionTitle,
    string EmptyPlayersText,
    bool ShowPlayerList,
    bool ShowDaysRunning,
    bool ShowTimestamp,
    int MaxPlayersShown,
    bool InlineSummaryFields,
    string PlayerSortMode,
    int UpdateIntervalSeconds,
    uint OnlineColor,
    uint OfflineColor);

internal sealed record ServerInfoOptions(
    string DisplayName,
    string IpPortDisplayOverride,
    DateTimeOffset? ResetStartDateUtc);

internal sealed record PermissionOptions(
    IReadOnlySet<ulong> AdminRoleIds,
    IReadOnlySet<ulong> ModeratorRoleIds);

internal sealed class ConfigurationService
{
    private const string TokenEnvironmentVariable = "SANGUINERELAY_DISCORD_TOKEN";
    private readonly ConfigFile _config;
    private readonly ManualLogSource _log;

    public ConfigurationService(ConfigFile config, ManualLogSource log)
    {
        _config = config;
        _log = log;
    }

    public RelayOptions Load()
    {
        var configuredToken = Bind("Discord", "BotToken", string.Empty, "Discord bot token. Prefer SANGUINERELAY_DISCORD_TOKEN.");
        var environmentToken = Environment.GetEnvironmentVariable(TokenEnvironmentVariable);
        var token = string.IsNullOrWhiteSpace(environmentToken) ? configuredToken.Trim() : environmentToken.Trim();

        var options = new RelayOptions(
            new GeneralOptions(
                Bind("General", "Enabled", true, "Enable SanguineRelay."),
                Bind("General", "DebugLogging", false, "Enable diagnostic logging.")),
            new DiscordOptions(
                Bind("Discord", "Enabled", true, "Enable Discord integration."),
                token,
                Id("Discord", "GuildId"),
                Id("Discord", "ChatChannelId"),
                Id("Discord", "PlayerEventsChannelId"),
                Id("Discord", "PlayerDeathChannelId"),
                Id("Discord", "PvPChannelId"),
                Id("Discord", "VBloodChannelId"),
                Id("Discord", "RaidChannelId"),
                Id("Discord", "ServerEventsChannelId"),
                Id("Discord", "AdminLogChannelId")),
            new ChatOptions(
                Bind("Chat", "EnableGameToDiscord", true, "Relay game chat to Discord."),
                Bind("Chat", "EnableDiscordToGame", true, "Relay Discord chat to the game."),
                Bind("Chat", "RelayGlobalChat", true, "Relay global chat."),
                Bind("Chat", "RelayLocalChat", false, "Relay local chat."),
                Bind("Chat", "RelayClanChat", false, "Relay clan chat. Enable only after reviewing privacy expectations."),
                Bind("Chat", "DiscordChatPrefix", "[Discord]", "Prefix shown in game for Discord chat."),
                Bind("Chat", "DiscordChatFormat", "{prefix} {user}: {message}", "Discord-to-game message template."),
                Bind("Chat", "AnnouncementPrefix", "[SERVER]", "Prefix shown for staff announcements."),
                Range("Chat", "MaxDiscordMessageLength", 300, 1, 500),
                Range("Chat", "DiscordChatCooldownMilliseconds", 750, 250, 60_000),
                Bind("Chat", "IgnoreDiscordBots", true, "Ignore all third-party bot messages. The SanguineRelay bot, webhooks, system messages, and configured ignored IDs are always ignored."),
                IdSet("Chat", "IgnoredDiscordUserIds", "User IDs that are always ignored, including when bot relay is enabled."),
                IdSet("Chat", "IgnoredDiscordBotIds", "Bot IDs that are always ignored when third-party bot relay is enabled."),
                Bind("Chat", "AllowGameMentionsInDiscord", false, "Allow game chat to create Discord mentions.")),
            new EventOptions(
                Bind("Events", "PlayerJoin", true, "Send player join notifications."),
                Bind("Events", "PlayerLeave", true, "Send player leave notifications."),
                Bind("Events", "PlayerDeaths", false, "Send player death notifications."),
                Bind("Events", "PvPKills", true, "Send PvP kill notifications."),
                Bind("Events", "VBloodKills", true, "Send V Blood kill notifications."),
                Bind("Events", "CastleRaids", false, "Send optional castle breach notifications; live runtime validation is required."),
                Bind("Events", "ServerLifecycleEvents", true, "Send server lifecycle notifications."),
                Range("Events", "ReconnectSuppressionSeconds", 15, 0, 300),
                Bind("Events", "JoinMessage", "🟢 {player} joined Vardoran.", "Player join template."),
                Bind("Events", "LeaveMessage", "🔴 {player} left Vardoran.", "Player leave template."),
                Bind("Events", "PlayerDeathMessage", "☠ {player} died.", "Player death template."),
                Bind("Events", "PvPKillMessage", "⚔ {killer} killed {victim}.", "PvP kill template."),
                Bind("Events", "VBloodMessage", "🩸 {player} defeated {boss}.", "V Blood kill template."),
                Bind("Events", "RaidMessage", "🔥 Castle belonging to {player} was breached by {killer}.", "Castle breach template.")),
            new PresenceOptions(
                Bind("DiscordPresence", "Enabled", true, "Enable bot presence updates."),
                Choice("DiscordPresence", "OnlineStatus", "Online", "Online", "Idle", "DoNotDisturb", "Invisible"),
                Choice("DiscordPresence", "ActivityType", "Watching", "Playing", "Watching", "Listening", "Competing"),
                Bind("DiscordPresence", "OnlineFormat", "{online} players online", "Presence template when populated."),
                Bind("DiscordPresence", "EmptyFormat", "No-one is online", "Presence template when empty."),
                Bind("DiscordPresence", "OfflineFormat", "V Rising server offline", "Presence template when offline."),
                Range("DiscordPresence", "UpdateIntervalSeconds", 60, 15, 3600)),
            new VoiceCounterOptions(
                Bind("VoicePlayerCounter", "Enabled", false, "Enable voice-channel population counter."),
                Id("VoicePlayerCounter", "ChannelId"),
                Bind("VoicePlayerCounter", "Format", "Players Online: {online}/{max}", "Voice counter template."),
                Bind("VoicePlayerCounter", "EmptyFormat", "Players Online: 0/{max}", "Empty voice counter template."),
                Bind("VoicePlayerCounter", "OfflineFormat", "V Rising: Offline", "Offline voice counter template."),
                Range("VoicePlayerCounter", "UpdateIntervalSeconds", 300, 60, 3600),
                Bind("VoicePlayerCounter", "LockChannel", true, "Persistently allow View Channel and deny Connect/Speak for @everyone while preserving every unrelated overwrite value.")),
            new StatusEmbedOptions(
                Bind("StatusEmbed", "Enabled", true, "Enable persistent server-status embed."),
                Id("StatusEmbed", "ChannelId"),
                Id("StatusEmbed", "MessageId"),
                Bind("StatusEmbed", "AutoCreateMessage", true, "Create the status message when none is configured."),
                Bind("StatusEmbed", "AutoRecreateDeletedMessage", true, "Recreate a deleted status message."),
                Bind("StatusEmbed", "TitleFormat", "{server}", "Status embed title template."),
                Bind("StatusEmbed", "ShowIpPort", true, "Show the configured public address."),
                Bind("StatusEmbed", "IpPortLabel", "IP and Port", "Address field label."),
                Bind("StatusEmbed", "OnlineCountLabel", "Online count", "Population field label."),
                Bind("StatusEmbed", "DaysRunningLabel", "Days running", "Days-running field label."),
                Bind("StatusEmbed", "PlayersSectionTitle", "Online players", "Player-list field label."),
                Bind("StatusEmbed", "EmptyPlayersText", "No-one is online", "Empty player-list text."),
                Bind("StatusEmbed", "ShowPlayerList", true, "Show player names."),
                Bind("StatusEmbed", "ShowDaysRunning", true, "Show days since reset, or successful world initialization when unset."),
                Bind("StatusEmbed", "ShowTimestamp", true, "Use a native Discord embed timestamp."),
                Range("StatusEmbed", "MaxPlayersShown", 50, 1, 200),
                Bind("StatusEmbed", "InlineSummaryFields", true, "Render summary fields inline."),
                Choice("StatusEmbed", "PlayerSortMode", "Alphabetical", "Alphabetical", "JoinOrder", "None"),
                Range("StatusEmbed", "UpdateIntervalSeconds", 60, 15, 3600),
                Color("StatusEmbed", "OnlineColor", "#00A651"),
                Color("StatusEmbed", "OfflineColor", "#D32F2F")),
            new ServerInfoOptions(
                Bind("ServerInfo", "ServerDisplayName", string.Empty, "Public server name override."),
                Bind("ServerInfo", "IpPortDisplayOverride", string.Empty, "Public address shown by status features."),
                Date("ServerInfo", "ResetStartDateUtc")),
            new PermissionOptions(
                IdSet("DiscordPermissions", "AdminRoleIds"),
                IdSet("DiscordPermissions", "ModeratorRoleIds")));

        Validate(options);
        _config.Save();
        return options;
    }

    private T Bind<T>(string section, string key, T defaultValue, string description) =>
        _config.Bind(section, key, defaultValue, description).Value;

    private int Range(string section, string key, int defaultValue, int minimum, int maximum)
    {
        var value = Bind(section, key, defaultValue, $"Allowed range: {minimum}-{maximum}.");
        if (value >= minimum && value <= maximum)
        {
            return value;
        }

        var corrected = Math.Clamp(value, minimum, maximum);
        _log.LogWarning($"[{section}] {key}={value} is outside {minimum}-{maximum}; using {corrected}.");
        return corrected;
    }

    private ulong Id(string section, string key)
    {
        var text = Bind(section, key, string.Empty, "Discord snowflake ID; leave empty to disable this target.").Trim();
        if (text.Length == 0)
        {
            return 0;
        }

        if (ulong.TryParse(text, out var id) && id != 0)
        {
            return id;
        }

        _log.LogWarning($"[{section}] {key} is not a valid Discord ID; the affected feature is disabled.");
        return 0;
    }

    private IReadOnlySet<ulong> IdSet(string section, string key, string description = "Comma-separated Discord snowflake IDs.")
    {
        var text = Bind(section, key, string.Empty, description);
        var values = new HashSet<ulong>();
        foreach (var part in text.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        {
            if (ulong.TryParse(part, out var id) && id != 0)
            {
                values.Add(id);
            }
            else
            {
                _log.LogWarning($"[{section}] {key} contains an invalid Discord ID; that value was ignored.");
            }
        }

        return values;
    }

    private DateTimeOffset? Date(string section, string key)
    {
        var text = Bind(section, key, string.Empty, "UTC reset date in ISO 8601 format.").Trim();
        if (text.Length == 0)
        {
            return null;
        }

        if (DateTimeOffset.TryParse(text, null, System.Globalization.DateTimeStyles.AssumeUniversal, out var date))
        {
            return date.ToUniversalTime();
        }

        _log.LogWarning($"[{section}] {key} is not a valid ISO 8601 date; process uptime will be used.");
        return null;
    }

    private uint Color(string section, string key, string defaultValue)
    {
        var value = Bind(section, key, defaultValue, "Six-digit hexadecimal color, for example #00A651.").Trim().TrimStart('#');
        if (value.Length == 6 && uint.TryParse(value, System.Globalization.NumberStyles.HexNumber, null, out var color))
        {
            return color;
        }

        _log.LogWarning($"[{section}] {key} is invalid; using {defaultValue}.");
        return uint.Parse(defaultValue.TrimStart('#'), System.Globalization.NumberStyles.HexNumber);
    }

    private string Choice(string section, string key, string defaultValue, params string[] allowedValues)
    {
        var value = Bind(section, key, defaultValue, $"Allowed values: {string.Join(", ", allowedValues)}.").Trim();
        var match = allowedValues.FirstOrDefault(allowed => allowed.Equals(value, StringComparison.OrdinalIgnoreCase));
        if (match is not null)
        {
            return match;
        }

        _log.LogWarning($"[{section}] {key} is invalid; using {defaultValue}.");
        return defaultValue;
    }

    private void Validate(RelayOptions options)
    {
        if (!options.Discord.Enabled)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(options.Discord.Token))
        {
            _log.LogError("Discord is enabled but no bot token is configured. Discord features will remain disabled.");
        }

        if (options.Discord.GuildId == 0)
        {
            _log.LogError("Discord is enabled but GuildId is missing or invalid. Discord features will remain disabled.");
        }
    }
}
