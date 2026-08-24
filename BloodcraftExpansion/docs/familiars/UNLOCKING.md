# Unlocking Familiars

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

When `FamiliarSystem` is enabled, defeating an eligible enemy can unlock that unit for the player.

## Stable defaults

| Setting | Default | Meaning |
|---|---:|---|
| `UnitUnlockChance` | `0.05` | Normal eligible-unit unlock chance. |
| `VBloodUnlockChance` | `0.01` | V Blood unlock chance when V Bloods are allowed. |
| `AllowVBloods` | `false` | V Bloods, including shard bearers, are excluded by default. |
| `AllowMinions` | `false` | Minion units are excluded by default because some behave poorly. |
| `ShareUnlocks` | `false` | Nearby clan/party sharing is disabled by default. |

Administrators can block specific PrefabGUIDs with `BannedUnits` or whole categories with `BannedTypes` (`Human`, `Undead`, `Demon`, `Mechanical`, `Beast`). Category bans take precedence over otherwise allowed units.

When sharing is enabled, Bloodcraft uses the leveling share distance. PvP sharing is clan-only; PvE can include eligible clan or party members.

## Player workflow

Use `.fam l` after an unlock, `.fam s [Name]` to search all boxes, and `.fam sb [Name]` to search and bind. Unlocking does not automatically make every unit safe or desirable; server bans remain authoritative.
---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
