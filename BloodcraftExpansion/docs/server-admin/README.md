# Server Administration

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

Bloodcraft is a server-side BepInEx plugin. Administrators control its systems through `BepInEx/config/io.zfolmt.Bloodcraft.cfg`, source-defined admin commands, and the persisted JSON data under `BepInEx/config/Bloodcraft/`.

## Operating path

1. Install the stable Bloodcraft package and its exact required dependencies.
2. Start the dedicated server once to generate configuration and data paths.
3. Stop the server before editing configuration.
4. Enable only the systems the server intends to support.
5. Restart and review the BepInEx log for Bloodcraft startup errors.
6. Back up configuration and player data before upgrades or bulk admin operations.

## Guides

- [Recommended Configuration](RECOMMENDED-CONFIG.md)
- [PvE Example](PVE-CONFIG.md)
- [PvP Example](PVP-CONFIG.md)
- [High Progression Example](HIGH-PROGRESSION-CONFIG.md)
- [Admin Commands](ADMIN-COMMANDS.md)
- [Player Data](PLAYER-DATA.md)
- [Troubleshooting](TROUBLESHOOTING.md)

---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
