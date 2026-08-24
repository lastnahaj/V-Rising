# Server Setup

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

Bloodcraft’s feature switches default to off for most major systems. Plan the progression model before enabling everything at once.

## Safe setup sequence

1. Install and launch once.
2. Confirm `Loaded [1.13.22]` and startup health in the server log.
3. Stop the server and back up `BepInEx/config/io.zfolmt.Bloodcraft.cfg` plus `BepInEx/config/Bloodcraft/`.
4. Enable foundational systems first: leveling, expertise, legacies, professions, classes, quests, and/or familiars.
5. Enable prestige only with leveling; review the separate expertise and legacy prestige caps.
6. Keep experimental features disabled until tested on a copy of the server.
7. Restart and verify with `.misc health`.

## Recommended first review

| Area | Keys to inspect first |
|---|---|
| Leveling | `LevelingSystem`, `MaxLevel`, `StartingLevel`, gain multipliers, `ExpShare` |
| Expertise | `ExpertiseSystem`, `MaxExpertiseLevel`, `ExpertiseStatChoices` |
| Legacies | `LegacySystem`, `MaxBloodLevel`, `LegacyStatChoices` |
| Classes | `ClassSystem`, `SynergyMultiplier`, `ClassOnHitEffects`, class-change cost |
| Familiars | `FamiliarSystem`, unlock chances, eligibility, PvP behavior |
| Prestige | `PrestigeSystem`, maximum prestiges, rate/stat multipliers |
| Quests | `QuestSystem`, reward pools, reroll costs |

Use [Recommended Configuration](../server-admin/RECOMMENDED-CONFIG.md) for a decision checklist and [Configuration Reference](../reference/CONFIGURATION.md) for exact defaults.
---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
