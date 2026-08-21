# Troubleshooting

Start with the earliest SanguineRelay warning or error in `BepInEx/LogOutput.log`. Share only relevant lines and redact tokens, authorization headers, private channel names, user IDs, and non-public server addresses.

## Bot appears offline or Discord connection fails

1. Confirm `General.Enabled` and `Discord.Enabled` are `true`.
2. Confirm the token belongs to the invited bot and has no quotes or accidental whitespace.
3. If using `SANGUINERELAY_DISCORD_TOKEN`, confirm the dedicated-server process—not only an interactive shell—receives it.
4. Confirm `GuildId` identifies a guild containing the bot.
5. Confirm the host can reach Discord over HTTPS and WebSocket connections.
6. Reset the token in the Developer Portal if exposure is suspected.

## Invalid guild or channel ID

- Enable Discord Developer Mode and copy the numeric ID again.
- Confirm the channel belongs to `GuildId`.
- Confirm a text-channel setting does not point to a category, forum, thread, or voice channel.
- Confirm `VoicePlayerCounter.ChannelId` points to an existing voice channel.
- Remove separators, quotes, and channel mention syntax such as `<#...>`.

## Slash commands do not appear

1. Confirm the bot was invited with the `applications.commands` scope.
2. Confirm it is connected to the configured guild.
3. Check startup logs for command reconciliation failures.
4. Confirm Discord displays the bot application expected for SanguineRelay.
5. Reinvite the bot with the correct scopes if the original invitation omitted application commands.

SanguineRelay creates or updates only `/status`, `/players`, `/player`, `/announce`, and `/relay-status`. It does not delete unrelated commands.

## Game messages reach Discord, but Discord messages do not reach V Rising

1. Set `Chat.EnableDiscordToGame = true`.
2. Enable Message Content Intent in the Discord Developer Portal.
3. Confirm the message is in exactly `Discord.ChatChannelId`.
4. Confirm the bot can view the channel and read ordinary messages.
5. Confirm the sender is not the SanguineRelay bot, a webhook, a system message, or a configured ignored ID.
6. If the sender is another bot, set `IgnoreDiscordBots = false` and ensure its ID is not ignored.
7. Wait for the configured per-user cooldown before retesting.
8. Inspect game-thread queue timeout/rejection metrics with `/relay-status`.

## Discord messages reach V Rising, but game chat does not reach Discord

1. Set `Chat.EnableGameToDiscord = true`.
2. Enable the intended chat type: global, local, or clan.
3. Confirm `Discord.ChatChannelId` is correct.
4. Grant View Channels and Send Messages in that channel.
5. Review `docs/GAME_HOOKS.md`; live compatibility is required after V Rising updates.

## Bot reports missing permissions

Grant only the permission required by the failing feature:

- Chat/events/audits: View Channels and Send Messages
- Embeds: Embed Links
- Persistent status recovery: Read Message History
- Slash commands: Use Application Commands and the OAuth2 `applications.commands` scope
- Voice counter: Manage Channels

Discord channel overwrites can deny a permission even when the bot role grants it. Do not grant Administrator as a troubleshooting shortcut.

## Status embed does not update

1. Confirm `StatusEmbed.Enabled = true` and `ChannelId` is a text channel.
2. Grant View Channels, Send Messages, Embed Links, and Read Message History.
3. Confirm `UpdateIntervalSeconds` is within `15`–`3600`.
4. If the message was deleted, confirm `AutoRecreateDeletedMessage = true`.
5. If an irrecoverably wrong message ID is persisted, stop the server before removing that ID from `BepInEx/config/SanguineRelay/state.json`.
6. Check Discord queue retries and permanent failures with `/relay-status`.

The bot edits one persistent message. It should not post a new status message on every refresh.

## Voice counter does not update

1. Confirm `VoicePlayerCounter.Enabled = true`.
2. Confirm `ChannelId` points to a voice channel created by an administrator.
3. Grant Manage Channels.
4. Confirm `UpdateIntervalSeconds` is within `60`–`3600`.
5. Allow for Discord rename rate limits and SanguineRelay's coalescing interval.
6. If `LockChannel = true`, confirm the bot can modify the `@everyone` overwrite.

The lock changes only View Channel, Connect, and Speak, preserves all other overwrite values, and persists until an administrator changes it.

## Player count is stale

- Confirm the V Rising world initialized successfully before checking uptime or population.
- Join and leave from a test client and inspect the hook status/documentation.
- Allow for `ReconnectSuppressionSeconds` on leave events and feature-specific Discord update intervals.
- Compare `/players`, `/status`, the persistent embed, and `/relay-status`; they should read the same immutable cached snapshot.
- Treat discrepancies as a runtime compatibility defect and stop rollout.

## Discord rate-limit warnings

SanguineRelay uses Discord-provided retry timing when available, caps retry delay and attempt counts, and avoids retrying ambiguous non-idempotent message sends. Short delays during bursts are expected. Persistent queue rejection, retry, or permanent-failure growth is not expected under normal load and should block rollout.

## Configuration parse errors

- Compare the live file against [`config-example.cfg`](../config-example.cfg) and [`CONFIGURATION.md`](CONFIGURATION.md).
- Use plain numeric Discord IDs without mention syntax.
- Use one of the documented case-insensitive enum values.
- Use six hexadecimal digits for colors.
- Use an ISO 8601 UTC value for `ResetStartDateUtc`.
- Restore the last known-good file rather than deleting production configuration blindly.

## Status embed was deleted

With `AutoRecreateDeletedMessage = true`, the worker attempts to create a replacement during normal operation and persists its ID. It avoids uncertain creation retries and does not create a new message during shutdown. If recovery fails, verify channel permissions and inspect the persisted state while the server is stopped.

## Clean shutdown does not show offline state

Offline presence, embed, voice name, and lifecycle output are best-effort within a bounded shutdown window. If Discord is unavailable, shutdown continues. Verify normal connected shutdown first, then test an intentional Discord outage and confirm the game process does not hang.

## Requesting support

Open a [GitHub issue](https://github.com/lastnahaj/V-Rising/issues) with versions, enabled modules, reproduction steps, and sanitized SanguineRelay log lines. Never include the bot token or a populated production configuration.
