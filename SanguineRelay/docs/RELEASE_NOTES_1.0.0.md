# SanguineRelay v1.0.0

SanguineRelay is a dedicated-server V Rising Discord bridge providing two-way chat, live population, Discord presence, a persistent status embed, an optional player-count voice channel, server events, slash commands, role-based authorization, and private administrative auditing.

The server administrator installs SanguineRelay through BepInEx and configures its Discord bot connection.

## Highlights

- V Rising global chat ↔ Discord channel chat
- Live population shared across presence, status, counters, and commands
- Persistent status message with online/offline state and optional player list
- Optional voice-channel player counter
- Join, leave, player death, PvP, V Blood, castle-breach, and lifecycle event support
- `/status`, `/players`, `/player`, `/announce`, and `/relay-status`
- Moderator/admin role IDs and complete privileged-command audit outcomes
- Bounded, observable game-thread and Discord queues
- Centralized mention, Markdown, Unicode, and length sanitization

## Requirements

- V Rising dedicated server 1.1.x for PC; intended for the 1.1.13.0 server line
- BepInExPack V Rising 1.733.2 / BepInEx IL2CPP 6.0.0-be.733
- Discord application and bot
- Message Content Intent only when Discord-to-game channel chat is enabled

SanguineRelay targets .NET 6 and includes the eight Discord/runtime dependency DLLs required by the plugin.

## Installation

1. Stop the dedicated server.
2. Install BepInExPack V Rising 1.733.2.
3. Extract `SanguineRelay-v1.0.0.zip` into the V Rising dedicated-server root.
4. Start once to generate the BepInEx configuration, then stop.
5. Follow the repository's Discord setup and configuration documentation.
6. Start the server and complete the staged live-test checklist before production rollout.

## Upgrade notes

This is the initial release candidate. For future updates, stop the server, back up configuration/state, replace the complete `BepInEx/plugins/SanguineRelay/` directory contents, review `CHANGELOG.md`, and preserve the live configuration and state files.

## Known limitations

- Live V Rising and Discord runtime verification is still pending for 1.0.0.
- All game hooks must be revalidated after a V Rising update.
- Castle-breach reporting is disabled by default and is the most update-sensitive event hook.
- Voice-counter channel creation is manual.
- RCON, kick/ban/restart, account linking, webhook-style player avatars, client-side features, and hot reload are not implemented.

## Integrity

```text
SanguineRelay-v1.0.0.zip
SHA-256: 3430EFBB7F328FE1E12F4305E2EC449422A749E327E200565CB54AA2E4D55B7F
```

The release package passed its exact-manifest, ZIP-path, forbidden-artifact, credential-pattern, ASCII privacy, and UTF-16LE privacy checks. No PDB, compile reference, BepInEx, Harmony, Il2CppInterop, ProjectM, Unity, or V Rising assembly is included.
