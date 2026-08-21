# Discord setup

This guide creates a least-privilege Discord application for SanguineRelay. A dedicated application is recommended so its commands, token, permissions, and ownership are isolated from unrelated bots.

## 1. Create the application and bot

1. Open the [Discord Developer Portal](https://discord.com/developers/applications).
2. Select **New Application**, give it a recognizable name, and create it.
3. Open **Bot** and select **Add Bot** or create the bot user when prompted.
4. Under the token controls, select **Reset Token** if necessary and copy it once.
5. Store the token in a password manager until it is installed on the server.

Never paste the token into an issue, chat, screenshot, source file, or committed configuration. Reset it immediately if exposed.

## 2. Configure gateway intents

Under **Bot → Privileged Gateway Intents**:

- Enable **Message Content Intent** when `Chat.EnableDiscordToGame = true`. SanguineRelay needs it to read ordinary messages in the configured Discord chat channel.
- Do not enable **Server Members Intent**; SanguineRelay does not request or use it.
- Do not enable **Presence Intent**; SanguineRelay publishes its own presence and does not consume guild-member presence.

Game-to-Discord-only installations can disable Message Content Intent and set `EnableDiscordToGame = false`.

## 3. Invite the bot

1. Open **OAuth2 → URL Generator**.
2. Select the `bot` scope.
3. Select the `applications.commands` scope.
4. Select the minimum permissions required by the enabled features.
5. Open the generated URL and choose the target guild.

| Permission | Required when |
| --- | --- |
| View Channels | Any configured Discord channel is used. |
| Send Messages | Chat, events, lifecycle messages, or audits are enabled. |
| Embed Links | Status embeds or embed-based commands are used. |
| Read Message History | A persistent status message is created/adopted/edited. |
| Use Application Commands | Slash commands are used. |
| Manage Channels | The optional voice player counter renames or locks a channel. |

Do not grant Administrator. Use channel-specific permission overrides if the bot should see only SanguineRelay channels.

## 4. Copy Discord IDs

1. Open Discord **User Settings → Advanced**.
2. Enable **Developer Mode**.
3. Right-click the relevant object and select **Copy ID**:

| Object | Where to copy it | SanguineRelay setting |
| --- | --- | --- |
| Guild/server | Server icon or name | `Discord.GuildId` |
| Text channel | Channel name | The corresponding `Discord.*ChannelId` or `StatusEmbed.ChannelId` |
| Voice channel | Channel name | `VoicePlayerCounter.ChannelId` |
| Role | Server Settings → Roles | `AdminRoleIds` or `ModeratorRoleIds` |
| User/bot | User profile/context menu | `IgnoredDiscordUserIds` or `IgnoredDiscordBotIds` |
| Message | Message context menu | Optional `StatusEmbed.MessageId` |

Names are not accepted in ID fields. IDs normally contain 17–20 digits.

## 5. Store the token

The recommended token source is the server process environment:

```powershell
$env:SANGUINERELAY_DISCORD_TOKEN = 'replace-locally'
```

Configure the variable in the Windows service account, service wrapper, or launch script that starts the dedicated server. The environment variable takes precedence over the BepInEx `BotToken` setting.

If service environment variables are unavailable, populate `Discord.BotToken` only in the private live BepInEx configuration. Restrict filesystem access and never copy that file into source control.

## 6. Configure channels

A practical starting layout is:

```text
V RISING
  #vrising-chat
  #vrising-events
  #vrising-pvp
  #vrising-bosses
  #vrising-raids
  #vrising-status

SERVER STATUS
  🔊 Players Online: 17/60

STAFF
  #vrising-admin-log
```

This is a recommendation, not a required layout. The same text channel can receive multiple output types, and an empty optional channel ID disables that destination.

Keep the admin log private. The voice counter must be created manually in 1.0.0; SanguineRelay only renames it and optionally updates the `@everyone` overwrite.

## 7. Configure roles

Add immutable role IDs to the live configuration:

```ini
[DiscordPermissions]
AdminRoleIds = 123456789012345678
ModeratorRoleIds = 234567890123456789
```

- Moderator roles may use `/player` and `/announce`.
- Administrator roles inherit moderator access and may use `/relay-status`.
- `/status` and `/players` are available to everyone in the configured guild.

SanguineRelay does not authorize by role name, username, or nickname.

## 8. First connection checklist

1. Start the V Rising server.
2. Confirm the BepInEx log reports the SanguineRelay Discord connection without exposing the token.
3. Confirm exactly five guild commands appear: `/status`, `/players`, `/player`, `/announce`, and `/relay-status`.
4. Run `/status`.
5. Send a normal message in `ChatChannelId` and confirm it appears in V Rising as prefixed chat, not a command.
6. Send V Rising global chat and confirm it reaches Discord without mentions.
7. Test an authorized and denied staff command and verify both appear in the private audit channel.
8. Verify the status embed and optional voice counter.

All V Rising hooks remain runtime-pending until this checklist is performed on an actual compatible dedicated server.
