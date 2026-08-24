# Experience Leveling

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

## What it is

Bloodcraft can replace gear-score-only progression with persistent character experience. Players gain most XP from enemy kills; V Bloods, docile units, war-event units, spawners, groups, and large level gaps each have separate multipliers.

## Progression

- Default starting level: `10`.
- Default maximum level: `90`.
- Unit and V Blood multipliers default to `7.5` and `15`.
- `LevelScalingMultiplier` reduces rewards for large player-versus-unit level gaps; `0` disables that reduction.
- `ExpShare` shares kill XP with nearby eligible combat participants. On PvP servers it is clan-only; level-range exemptions apply as described by the source configuration.
- Leveling prestige resets level progress and modifies later progression rates.

## Rested XP

When `RestedXPSystem` is enabled, offline time in a coffin can build rested XP. Stone coffins grant the full configured accumulation and wooden coffins half. The default cap is five levels of bonus progress, with a five-percent tick every 120 minutes. Leveling prestige clears accumulated rested XP.

## Commands

| Command | Purpose |
|---|---|
| `.lvl get` | Show current level and experience progress. |
| `.lvl log` | Toggle leveling progress messages. |
| `.lvl set [Player] [Level]` | Admin: set a player level. |
| `.lvl ignore [Player]` | Admin: toggle shared-XP eligibility for a player. |

## Admin notes

Tune `MaxLevel`, gain multipliers, share distance, and scaling together. Raising only the cap can make the final levels disproportionately slow. Use the [leveling configuration table](../reference/CONFIG-LEVELING.md) and [leveling guide](../progression/LEVELING-GUIDE.md).
---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
