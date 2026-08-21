# SanguineRelay

SanguineRelay connects a V Rising dedicated server to Discord. It provides a two-way chat bridge, live server information, event notifications, status displays, and Discord administration commands.

Copyright (c) 2026 Shikaru x InfiniteGamingServers. All Rights Reserved.

## Release status

Version 1.0.0 is available as a prerelease. The project builds and tests successfully, but its V Rising hooks still require live validation on the target server build before production use.

[Download v1.0.0](https://github.com/lastnahaj/V-Rising/releases/tag/v1.0.0) · [Hook verification checklist](docs/GAME_HOOKS.md) · [Troubleshooting](docs/TROUBLESHOOTING.md)

## Features

- V Rising global chat relayed to Discord
- Discord channel messages relayed to V Rising chat
- Live population shared by presence, status displays, counters, and commands
- Join, leave, death, PvP, V Blood, castle breach, and server lifecycle events
- Persistent status message with optional player list
- Optional voice-channel player counter
- `/status`, `/players`, `/player`, `/announce`, and `/relay-status`
- Discord role checks for moderator and administrator commands
- Private audit logging for privileged command outcomes
- Bounded game-thread and Discord queues with health counters

Castle breach notifications are disabled by default because that hook is especially sensitive to game updates.

## Requirements

- V Rising dedicated server 1.1.x for PC
- BepInExPack V Rising 1.733.2
- .NET 6 CoreCLR supplied by BepInExPack
- Discord application and bot

SanguineRelay is installed in the dedicated server's BepInEx plugin directory. The server administrator manages the plugin configuration and Discord bot credentials.

## Installation

1. Stop the dedicated server.
2. Install [BepInExPack V Rising 1.733.2](https://thunderstore.io/c/v-rising/p/BepInEx/BepInExPack_V_Rising/).
3. Download `SanguineRelay-v1.0.0.zip` from the [release page](https://github.com/lastnahaj/V-Rising/releases/tag/v1.0.0).
4. Extract the archive into the dedicated-server root. The plugin should be located at:

   ```text
   BepInEx/plugins/SanguineRelay/SanguineRelay.dll
   ```

5. Start the server once to generate `BepInEx/config/com.infinitegamingservers.sanguinerelay.cfg`, then stop it.
6. Configure the Discord bot token, guild, channels, roles, and enabled features.
7. Start the server and complete the [live hook checklist](docs/GAME_HOOKS.md).

The release archive includes the Discord runtime libraries required by the plugin. Do not copy development reference assemblies into the server plugin directory.

## Discord setup

The bot needs access to the configured guild and channels. Message Content Intent is required only when Discord-to-game channel chat is enabled.

Follow [Discord setup](docs/DISCORD-SETUP.md) for application creation, gateway intents, OAuth2 permissions, channel IDs, role IDs, and token storage.

Never commit or share a populated configuration file. Reset the bot token immediately if it is exposed.

## Configuration

Use [config-example.cfg](config-example.cfg) as a clean reference. The full [configuration guide](docs/CONFIGURATION.md) documents every section and validation rule.

Configuration changes require a server restart in version 1.0.0.

## Chat bridge

```text
V Rising                                  Discord
CrimsonWarden_47: Anyone doing Dracula?  →  CrimsonWarden_47: Anyone doing Dracula?

Discord                                   V Rising
MoonlitRook_83: I'll join.               →  [Discord] MoonlitRook_83: I'll join.
```

Discord messages are sanitized before they enter game chat. Mentions, Markdown control characters, excessive Unicode, and oversized messages are normalized. A leading `/` is neutralized and cannot execute a game or Discord command.

## Commands

| Command | Access | Purpose |
| --- | --- | --- |
| `/status` | Everyone | Display server state, population, uptime, and game version. |
| `/players` | Everyone | List cached online characters. |
| `/player` | Moderator | Look up an online character. |
| `/announce` | Moderator | Send a sanitized server announcement. |
| `/relay-status` | Administrator | Display connection, queue, dispatcher, and audit health. |

See the [command reference](docs/COMMANDS.md) for arguments, role checks, responses, and audit behavior.

## Events and status

Event destinations are configured independently. Supported event categories include player connections, deaths, PvP downs, V Blood completions, castle breaches, and server lifecycle changes.

The status message, bot presence, voice counter, and commands all use the same cached server snapshot. V Blood events are grouped briefly so a shared encounter produces one message rather than one message per participant.

## Security

- Bot tokens are treated as secrets and are never written to logs.
- Discord text is sanitized before reaching the game server.
- Role IDs control privileged command access.
- Privileged commands can be audited to a private Discord channel.
- Steam IDs and server coordinates are not published.
- Queue sizes, message lengths, and cooldowns are bounded.

RCON, kick, ban, restart, account linking, webhook-style player avatars, and hot reload are not implemented in version 1.0.0.

## Updating and removal

Before updating, stop the server and back up the configuration and SanguineRelay state files. Replace the complete `BepInEx/plugins/SanguineRelay/` directory with the files from the new release, then review [CHANGELOG.md](CHANGELOG.md).

To remove SanguineRelay, stop the server and delete its plugin directory, configuration file, and state directory. Discord messages and channels created by an administrator are not deleted automatically.

## Building

Development requires the .NET 8 SDK, BepInExPack V Rising compile references, and metadata-only V Rising reference assemblies.

```powershell
./scripts/bootstrap.ps1
dotnet restore SanguineRelay.sln --locked-mode --configfile NuGet.config
dotnet format SanguineRelay.sln --verify-no-changes --no-restore
dotnet build SanguineRelay.sln -c Release --no-restore
dotnet test SanguineRelay.sln -c Release --no-build --no-restore
./scripts/package.ps1
```

`package.ps1` produces the installable archive in `artifacts/` and runs the release privacy, credential, manifest, and ZIP-path checks.

## Support

Before opening an issue, review [troubleshooting](docs/TROUBLESHOOTING.md) and the [hook verification status](docs/GAME_HOOKS.md). Include the relevant versions, enabled modules, reproduction steps, and sanitized log lines. Never include tokens, private IDs, addresses, or populated production configuration.

## License and notices

SanguineRelay is proprietary software. See [COPYRIGHT](COPYRIGHT) for ownership terms and [THIRD_PARTY_NOTICES.md](THIRD_PARTY_NOTICES.md) for bundled dependency licenses.
