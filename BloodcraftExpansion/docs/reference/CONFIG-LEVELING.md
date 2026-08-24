# Leveling Configuration

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

## Leveling

| Setting | Type | Default | Description |
|---|---|---|---|
| `LevelingSystem` | bool | `False` | Enable or disable the leveling system. |
| `RestedXPSystem` | bool | `False` | Enable or disable rested experience for players logging out inside of coffins (half for wooden, full for stone). Prestiging level will reset accumulated rested xp. |
| `RestedXPRate` | float | `0.05` | Rate of Rested XP accumulation per tick (as a percentage of maximum allowed rested XP, if configured to one tick per hour 20 hours offline in a stone coffin will provide maximum current rested XP). |
| `RestedXPMax` | int | `5` | Maximum extra levels worth of rested XP a player can accumulate. |
| `RestedXPTickRate` | float | `120` | Minutes required to accumulate one tick of Rested XP. |
| `MaxLevel` | int | `90` | The maximum level a player can reach. |
| `StartingLevel` | int | `10` | Starting level for players if no data is found. |
| `UnitLevelingMultiplier` | float | `7.5` | The multiplier for experience gained from units. |
| `VBloodLevelingMultiplier` | float | `15` | The multiplier for experience gained from VBloods. |
| `DocileUnitMultiplier` | float | `0.15` | The multiplier for experience gained from docile units. |
| `WarEventMultiplier` | float | `0.2` | The multiplier for experience gained from war event trash spawns. |
| `UnitSpawnerMultiplier` | float | `0` | The multiplier for experience gained from unit spawners (vermin nests, tombs). Applies to familiar experience as well. |
| `GroupLevelingMultiplier` | float | `1` | The multiplier for experience gained from group kills. |
| `LevelScalingMultiplier` | float | `0.05` | Reduces experience gained from kills with a large level gap between player and unit, increase to make harsher decrease or set to 0 to remove. |
| `ExpShare` | bool | `True` | Enable or disable sharing experience with nearby players (ExpShareDistance) in combat that are within level range (ExpShareLevelRange, this does not apply to players that have prestiged at least once on PvE servers or clan members of the player that does the final blow) along with familiar unlock sharing if enabled (on PvP servers will only apply to clan members). |
| `ExpShareLevelRange` | int | `10` | Maximum level difference between players allowed for ExpShare, players who have prestiged at least once are exempt from this. Use 0 for no level diff restrictions. |
| `ExpShareDistance` | float | `25` | Default is ~5 floor tile lengths. |

Exact defaults are shown for stable 1.13.22. Only constraints explicitly stated in the descriptions should be treated as supported ranges.

---

[Wiki Home](../HOME.md) · [Commands](COMMANDS.md) · [Configuration](CONFIGURATION.md)
