# Server Administration

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.26 stable<br>
    **Last verified:** 2026-11-02<br>
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

| Guide | Use it for |
|---|---|
| [Recommended Rollout](RECOMMENDED-CONFIG.md) | A staged first deployment with high-risk systems disabled |
| [Cooperative PvE Profile](PVE-CONFIG.md) | Shared progression and conservative familiar behavior |
| [Conservative PvP Profile](PVP-CONFIG.md) | Lower proc/synergy pressure and a defined balance-test gate |
| [Long-Progression Profile](HIGH-PROGRESSION-CONFIG.md) | Slower seasonal progression with prestige enabled deliberately |
| [Admin Commands](ADMIN-COMMANDS.md) | Safe state-changing command procedure |
| [Player Data](PLAYER-DATA.md) | Backup, restore, and persistence boundaries |
| [Troubleshooting](TROUBLESHOOTING.md) | Startup, feature, Eclipse, and recovery diagnosis |

!!! warning "Profiles are overlays"
    Always generate the configuration with the installed package first. Merge reviewed keys into that file while the server is stopped; do not replace a newer generated configuration with an old example.

---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
