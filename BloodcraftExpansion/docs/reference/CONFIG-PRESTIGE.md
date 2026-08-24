# Prestige Configuration

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

## Prestige

| Setting | Type | Default | Description |
|---|---|---|---|
| `PrestigeSystem` | bool | `False` | Enable or disable the prestige system (requires leveling to be enabled as well). |
| `PrestigeBuffs` | string (PrefabGUID list) | `"1504279833,0,0,0,0,0,0,0,0,0"` | The PrefabGUID hashes for general prestige buffs, use 0 to skip otherwise buff applies at the prestige level (only shroud for first default while reworked). |
| `PrestigeLevelsToUnlockClassSpells` | string | `"0,1,2,3,4,5"` | The prestige levels at which class spells are unlocked. This should match the number of spells per class +1 to account for the default class spell. Can leave at 0 each if you want them unlocked from the start. |
| `MaxLevelingPrestiges` | int | `10` | The maximum number of prestiges a player can reach in leveling. |
| `LevelingPrestigeReducer` | float | `0.05` | Flat factor by which experience is reduced per increment of prestige in leveling. |
| `PrestigeRatesReducer` | float | `0.1` | Flat factor by which rates are reduced in expertise/legacy per increment of prestige in expertise/legacy. |
| `PrestigeStatMultiplier` | float | `0.1` | Flat factor by which stats are increased in expertise/legacy bonuses per increment of prestige in expertise/legacy. |
| `PrestigeRateMultiplier` | float | `0.1` | Flat factor by which rates are increased in expertise/legacy per increment of prestige in leveling. |
| `ExoPrestiging` | bool | `False` | Enable or disable exo prestiges (need to max normal prestiges first, 100 exo prestiges currently available). |
| `ExoPrestigeReward` | int | `28358550` | The reward for exo prestiging (tier 3 nether shards by default). |
| `ExoPrestigeRewardQuantity` | int | `500` | The quantity of the reward for exo prestiging. |
| `TrueImmortal` | bool | `False` | Enable or disable Immortal blood for the duration of exoform. |
| `Leaderboard` | bool | `True` | Enable or disable the various prestige leaderboard rankings. |

Exact defaults are shown for stable 1.13.22. Only constraints explicitly stated in the descriptions should be treated as supported ranges.

---

[Wiki Home](../HOME.md) · [Commands](COMMANDS.md) · [Configuration](CONFIGURATION.md)
