# Configuration reference

SanguineRelay writes its live BepInEx configuration to:

```text
BepInEx/config/com.infinitegaming.sanguinerelay.cfg
```

Use [`config-example.cfg`](../config-example.cfg) as a clean reference. Every setting is loaded at plugin startup; **all changes require a server restart** because version 1.0.0 does not support hot reload.

Discord IDs are unsigned snowflake IDs. An empty optional channel ID disables only that destination. Comma-separated role/user/bot ID lists accept empty values. Never commit a populated bot token.

## General

| Setting | Default | Accepted values | Description | Example |
| --- | --- | --- | --- | --- |
| `Enabled` | `true` | `true`, `false` | Enables the plugin. | `true` |
| `DebugLogging` | `false` | `true`, `false` | Enables additional diagnostic logging. | `false` |

## Discord

| Setting | Default | Accepted values | Description | Example |
| --- | --- | --- | --- | --- |
| `Enabled` | `true` | `true`, `false` | Enables the Discord client and all Discord features. | `true` |
| `BotToken` | empty | Discord bot token or empty | Fallback token source. Prefer `SANGUINERELAY_DISCORD_TOKEN`. | Leave empty when using the environment variable. |
| `GuildId` | empty | Discord guild ID | Guild where SanguineRelay registers commands and resolves channels. | `123456789012345678` |
| `ChatChannelId` | empty | Text-channel ID or empty | Two-way chat destination/source. | `123456789012345678` |
| `PlayerEventsChannelId` | empty | Text-channel ID or empty | Join and leave notifications. | `123456789012345678` |
| `PlayerDeathChannelId` | empty | Text-channel ID or empty | Player death notifications. | `123456789012345678` |
| `PvPChannelId` | empty | Text-channel ID or empty | PvP kill notifications. | `123456789012345678` |
| `VBloodChannelId` | empty | Text-channel ID or empty | V Blood kill notifications. | `123456789012345678` |
| `RaidChannelId` | empty | Text-channel ID or empty | Optional castle-breach notifications. | `123456789012345678` |
| `ServerEventsChannelId` | empty | Text-channel ID or empty | Server online/offline lifecycle notifications. | `123456789012345678` |
| `AdminLogChannelId` | empty | Private text-channel ID or empty | Privileged-command audit log. | `123456789012345678` |

The `SANGUINERELAY_DISCORD_TOKEN` process environment variable takes precedence over `BotToken` after surrounding whitespace is trimmed.

## Chat

| Setting | Default | Accepted values | Description | Example |
| --- | --- | --- | --- | --- |
| `EnableGameToDiscord` | `true` | `true`, `false` | Relays enabled V Rising chat types to Discord. | `true` |
| `EnableDiscordToGame` | `true` | `true`, `false` | Relays ordinary messages from `ChatChannelId` into V Rising. | `true` |
| `RelayGlobalChat` | `true` | `true`, `false` | Enables global-chat relay. | `true` |
| `RelayLocalChat` | `false` | `true`, `false` | Enables local-chat relay. | `false` |
| `RelayClanChat` | `false` | `true`, `false` | Enables clan-chat relay; review privacy expectations first. | `false` |
| `DiscordChatPrefix` | `[Discord]` | Text | Prefix rendered inside V Rising for Discord messages. | `[Discord]` |
| `DiscordChatFormat` | `{prefix} {user}: {message}` | Text template | Discord-to-game format. | `{prefix} {user}: {message}` |
| `AnnouncementPrefix` | `[SERVER]` | Text | Prefix used by `/announce`. | `[SERVER]` |
| `MaxDiscordMessageLength` | `300` | `1`–`500` | Maximum Discord-to-game message input length. | `300` |
| `DiscordChatCooldownMilliseconds` | `750` | `250`–`60000` | Per-user anti-spam interval. | `750` |
| `IgnoreDiscordBots` | `true` | `true`, `false` | Ignores all third-party bots when true. Own-bot, webhook, system, and configured-ID filtering always applies. | `true` |
| `IgnoredDiscordUserIds` | empty | Comma-separated user IDs | User IDs always excluded from relay. | `123456789012345678,234567890123456789` |
| `IgnoredDiscordBotIds` | empty | Comma-separated bot IDs | Bot IDs always excluded, including when third-party bot relay is enabled. | `123456789012345678` |
| `AllowGameMentionsInDiscord` | `false` | `true`, `false` | Unsafe opt-in allowing only game-to-Discord chat to create Discord mentions. | `false` |

Chat format placeholders are `{prefix}`, `{user}`, and `{message}`. Discord chat is always sanitized before entering the game and cannot execute commands.

## Events

| Setting | Default | Accepted values | Description | Example |
| --- | --- | --- | --- | --- |
| `PlayerJoin` | `true` | `true`, `false` | Sends player join notifications. | `true` |
| `PlayerLeave` | `true` | `true`, `false` | Sends delayed player leave notifications. | `true` |
| `PlayerDeaths` | `false` | `true`, `false` | Sends player death notifications. | `false` |
| `PvPKills` | `true` | `true`, `false` | Sends PvP kill notifications. | `true` |
| `VBloodKills` | `true` | `true`, `false` | Sends grouped V Blood kill notifications. | `true` |
| `CastleRaids` | `false` | `true`, `false` | Enables update-sensitive castle-breach reporting. | `false` |
| `ServerLifecycleEvents` | `true` | `true`, `false` | Sends server online/offline messages. | `true` |
| `ReconnectSuppressionSeconds` | `15` | `0`–`300` | Delays leave publication so quick reconnects do not create churn. | `15` |
| `JoinMessage` | `🟢 {player} joined Vardoran.` | Template with `{player}` | Player join text. | `🟢 {player} joined.` |
| `LeaveMessage` | `🔴 {player} left Vardoran.` | Template with `{player}` | Player leave text. | `🔴 {player} left.` |
| `PlayerDeathMessage` | `☠ {player} died.` | Template with `{player}` | Player death text. | `☠ {player} died.` |
| `PvPKillMessage` | `⚔ {killer} killed {victim}.` | Template with `{killer}`, `{victim}` | PvP kill text. | `⚔ {killer} defeated {victim}.` |
| `VBloodMessage` | `🩸 {player} defeated {boss}.` | Template with `{player}`, `{boss}` | V Blood text. | `🩸 {player} defeated {boss}.` |
| `RaidMessage` | `🔥 Castle belonging to {player} was breached by {killer}.` | Template with `{player}`, `{killer}` | Castle-breach text. | Keep the default until live verification. |

All game-derived replacement values use centralized Discord display sanitization. Castle reporting is optional, disabled by default, and runtime-pending.

## DiscordPresence

| Setting | Default | Accepted values | Description | Example |
| --- | --- | --- | --- | --- |
| `Enabled` | `true` | `true`, `false` | Enables population-based Discord presence. | `true` |
| `OnlineStatus` | `Online` | `Online`, `Idle`, `DoNotDisturb`, `Invisible` | Bot status while the server is online. | `Online` |
| `ActivityType` | `Watching` | `Playing`, `Watching`, `Listening`, `Competing` | Activity verb. | `Watching` |
| `OnlineFormat` | `{online} players online` | Status template | Text when at least one player is online. | `{online}/{max} vampires online` |
| `EmptyFormat` | `No-one is online` | Status template | Text when the server is online and empty. | `No-one is online` |
| `OfflineFormat` | `V Rising server offline` | Status template | Best-effort clean-shutdown text. | `V Rising server offline` |
| `UpdateIntervalSeconds` | `60` | `15`–`3600` | Minimum/periodic presence refresh interval. | `60` |

Presence, status-title, and voice templates support `{server}`, `{online}`, `{max}`, `{ip}`, `{days}`, and `{uptime}`.

## VoicePlayerCounter

| Setting | Default | Accepted values | Description | Example |
| --- | --- | --- | --- | --- |
| `Enabled` | `false` | `true`, `false` | Enables renaming of an existing voice channel. | `false` |
| `ChannelId` | empty | Voice-channel ID or empty | Existing channel to rename. SanguineRelay does not create it. | `123456789012345678` |
| `Format` | `Players Online: {online}/{max}` | Status template | Channel name while populated. | `Players Online: {online}/{max}` |
| `EmptyFormat` | `Players Online: 0/{max}` | Status template | Channel name while online and empty. | `Players Online: 0/{max}` |
| `OfflineFormat` | `V Rising: Offline` | Status template | Best-effort shutdown name. | `V Rising: Offline` |
| `UpdateIntervalSeconds` | `300` | `60`–`3600` | Minimum rename interval. | `300` |
| `LockChannel` | `true` | `true`, `false` | Sets View=Allow and Connect/Speak=Deny for `@everyone`, preserving every unrelated overwrite value. | `true` |

The lock persists until an administrator changes it. Shutdown and uninstallation do not restore an older overwrite.

## StatusEmbed

| Setting | Default | Accepted values | Description | Example |
| --- | --- | --- | --- | --- |
| `Enabled` | `true` | `true`, `false` | Enables persistent status publication. | `true` |
| `ChannelId` | empty | Text-channel ID or empty | Channel containing the persistent message. | `123456789012345678` |
| `MessageId` | empty | Message ID or empty | Existing status message to adopt. Generated IDs are persisted separately. | Leave empty for automatic creation. |
| `AutoCreateMessage` | `true` | `true`, `false` | Creates one status message when no ID is known. | `true` |
| `AutoRecreateDeletedMessage` | `true` | `true`, `false` | Recreates the message if the known message was deleted. | `true` |
| `TitleFormat` | `{server}` | Status template | Embed title. | `{server}` |
| `ShowIpPort` | `true` | `true`, `false` | Shows the configured public address field. | `false` for private servers. |
| `IpPortLabel` | `IP and Port` | Text | Address field label. | `Connect` |
| `OnlineCountLabel` | `Online count` | Text | Population field label. | `Players` |
| `DaysRunningLabel` | `Days running` | Text | Reset-age field label. | `Wipe day` |
| `PlayersSectionTitle` | `Online players` | Text | Player-list heading. | `Online players` |
| `EmptyPlayersText` | `No-one is online` | Text | Player-list text when empty. | `No-one is online` |
| `ShowPlayerList` | `true` | `true`, `false` | Shows cached character names. | `true` |
| `ShowDaysRunning` | `true` | `true`, `false` | Shows days since configured reset or world initialization. | `true` |
| `ShowTimestamp` | `true` | `true`, `false` | Adds and periodically refreshes a native Discord timestamp. | `true` |
| `MaxPlayersShown` | `50` | `1`–`200` | Maximum listed players before a remainder line. | `50` |
| `InlineSummaryFields` | `true` | `true`, `false` | Places address/population/day fields inline. | `true` |
| `PlayerSortMode` | `Alphabetical` | `Alphabetical`, `JoinOrder`, `None` | Player display ordering. | `Alphabetical` |
| `UpdateIntervalSeconds` | `60` | `15`–`3600` | Periodic embed evaluation interval. | `60` |
| `OnlineColor` | `#00A651` | Six-digit hexadecimal color | Online embed color. | `#00A651` |
| `OfflineColor` | `#D32F2F` | Six-digit hexadecimal color | Offline embed color. | `#D32F2F` |

## ServerInfo

| Setting | Default | Accepted values | Description | Example |
| --- | --- | --- | --- | --- |
| `ServerDisplayName` | empty | Public text or empty | Overrides the status/presence server name. | `Infinite Gaming V Rising` |
| `IpPortDisplayOverride` | empty | Intentionally public hostname/address or empty | Value displayed by status features; no automatic address discovery occurs. | `play.example.com` |
| `ResetStartDateUtc` | empty | ISO 8601 UTC date/time or empty | Wipe/reset origin for days running. Empty uses world initialization. | `2026-08-01T17:00:00Z` |

## DiscordPermissions

| Setting | Default | Accepted values | Description | Example |
| --- | --- | --- | --- | --- |
| `AdminRoleIds` | empty | Comma-separated role IDs | Grants administrator-level SanguineRelay commands. | `123456789012345678` |
| `ModeratorRoleIds` | empty | Comma-separated role IDs | Grants moderator commands. Admin roles inherit this level. | `234567890123456789` |

Authorization uses immutable Discord IDs, never role names, usernames, or nicknames. Keep `AdminLogChannelId` private and verify both allowed and denied actions appear there during staged testing.

## Validation behavior

- Invalid choices, IDs, dates, colors, and out-of-range values are logged and replaced with safe defaults where supported.
- A missing optional channel ID disables only the corresponding output.
- A missing/invalid token or guild ID prevents Discord startup without disabling the V Rising server.
- The token is redacted from Discord log messages, but administrators must still protect the environment and configuration file.
