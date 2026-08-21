# Command reference

SanguineRelay registers exactly five guild-scoped slash commands. It reconciles only these names and does not bulk-delete unrelated commands owned by the same Discord application.

## Permission levels

| Level | Configuration | Access |
| --- | --- | --- |
| Everyone | None | `/status`, `/players` |
| Moderator | `DiscordPermissions.ModeratorRoleIds` | Everyone commands plus `/player`, `/announce` |
| Administrator | `DiscordPermissions.AdminRoleIds` | All commands, including `/relay-status`; inherits moderator access |

Authorization uses Discord role IDs. Commands from another guild or a direct message are rejected.

## Commands

| Command | Arguments | Required access | Example | Response |
| --- | --- | --- | --- | --- |
| `/status` | None | Everyone | `/status` | Embed containing server state, population, uptime, days running, and game version. |
| `/players` | None | Everyone | `/players` | Embed containing the cached, alphabetically sorted online-character list. |
| `/player` | `name` — required exact online character name | Moderator | `/player name:CrimsonWarden_47` | Ephemeral embed with character name, online state, optional clan, and optional gear level. Steam IDs are not displayed. |
| `/announce` | `message` — required, 1–400 characters | Moderator | `/announce message:Server restart in ten minutes.` | Ephemeral acknowledgement after the sanitized announcement is executed on the game thread. |
| `/relay-status` | None | Administrator | `/relay-status` | Ephemeral health report for Discord connection, game state, population, feature state, uptime, dispatcher, outbound queue, and audit delivery. |

## Audit behavior

Every allowed and denied execution of `/player`, `/announce`, and `/relay-status` is audited when `Discord.AdminLogChannelId` is configured. Recorded outcomes include:

- Success
- Permission denial
- Validation failure
- Game-thread timeout
- Execution failure

An audit entry contains the Discord user ID, safely rendered display name, command, sanitized arguments, result, and UTC timestamp. Queue rejection and delivery failure counters are visible only through the administrator-only `/relay-status` response.

Never place passwords, tokens, private connection strings, or other secrets in slash-command arguments. Audit entries deliberately avoid tokens and authorization headers.

## Non-commands

Ordinary Discord chat relayed into V Rising cannot execute game commands. A leading slash is neutralized during sanitization. Version 1.0.0 does not implement `/rcon`, `/kick`, `/ban`, `/restart`, account linking, or `/setup-player-counter`.
