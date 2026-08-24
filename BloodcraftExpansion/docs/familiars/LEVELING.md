# Familiar Leveling

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

A bound familiar gains experience from combat and scales with its level. The default maximum is `90`.

| Setting | Default |
|---|---:|
| `MaxFamiliarLevel` | `90` |
| `UnitFamiliarMultiplier` | `7.5` |
| `VBloodFamiliarMultiplier` | `15` |
| `UnitSpawnerMultiplier` | `0` |

The shared `UnitSpawnerMultiplier` also applies to familiar XP, so spawner-based leveling is disabled by default.

Use `.fam gl` with an active familiar to display level, progress, prestige, name/shiny information, and the stats currently exposed by the stable command. Administrators can use `.fam sl [Player] [Level]` for the target player’s active familiar.

At maximum level, a familiar can prestige if `FamiliarPrestige` is enabled. Read [Familiar Prestige](PRESTIGE.md) before resetting progress.
---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
