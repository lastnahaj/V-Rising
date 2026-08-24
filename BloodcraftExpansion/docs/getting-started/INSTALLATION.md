# Installation

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

Bloodcraft is a **server mod**. Its stable Thunderstore package is `zfolmt-Bloodcraft-1.13.22`.

## Requirements

| Package | Stable dependency | Where |
|---|---:|---|
| BepInExPack for V Rising | `1.733.2` | Server |
| VampireCommandFramework | `0.10.4` | Server |
| Bloodcraft | `1.13.22` | Server |
| Eclipse | `1.3.14` | Optional client companion |

Install through a Thunderstore-compatible mod manager or place each package according to its own Thunderstore instructions. A manager normally resolves Bloodcraft’s two required dependencies. Do not install Eclipse on the dedicated server as a substitute for Bloodcraft; it is a client UI.

## First launch

1. Stop the dedicated server.
2. Install BepInEx, VampireCommandFramework, and Bloodcraft.
3. Start the server once and wait for Bloodcraft’s startup message.
4. Stop the server before editing configuration.
5. Edit `BepInEx/config/io.zfolmt.Bloodcraft.cfg`.
6. Restart and use `.misc health` as an administrator to inspect startup readiness.

Bloodcraft also creates player-data directories beneath `BepInEx/config/Bloodcraft/`. Treat those JSON files as live server data: back them up and do not hand-edit them while the server is running.

## Compatibility

The 1.13.22 source builds against V Rising references `1.1.12-r99041-b2`. This is evidence of its build target, not a promise that every later game build is compatible. Match the current stable package and read its changelog after any V Rising update.

For client UI, continue to [Eclipse](ECLIPSE.md). For server choices, continue to [Server Setup](SERVER-SETUP.md).
---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
