# General and Starter Kit Configuration

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

## General

| Setting | Type | Default | Description |
|---|---|---|---|
| `LanguageLocalization` | string (PrefabGUID list) | `"English"` | The language localization for prefabs displayed to users. English by default. Options: Brazilian, English, French, German, Hungarian, Italian, Japanese, Koreana, Latam, Polish, Russian, SimplifiedChinese, Spanish, TraditionalChinese, Thai, Turkish, Vietnamese |
| `Eclipsed` | bool | `False` | Eclipse will be active if any features that sync with the client are enabled. Instead, this now controls the frequency; true for faster (0.1s), false for slower (2.5s). |
| `UseEmberglassEclipseBridge` | bool | `False` | Use Emberglass for the Bloodcraft/Eclipse bridge when Emberglass is installed. Falls back to the legacy chat bridge when disabled or unavailable. |
| `ElitePrimalRifts` | bool | `False` | Enable or disable elite primal rifts. |
| `RiftFrequency` | int | `0` | Number of primal rifts to start per day when they are enabled (24 max). |
| `NightmareMode` | bool | `False` | Enable or disable PvE-only health, power, and movement tuning for enemy units. |
| `EliteShardBearers` | bool | `False` | Enable or disable elite shard bearers. |
| `ShardBearerLevel` | int | `0` | Sets level of shard bearers if elite shard bearers is enabled. Leave at 0 for no effect. |
| `PotionStacking` | bool | `False` | Enable or disable potion stacking (can have t01/t02 effects at the same time). |
| `BearFormDash` | bool | `False` | Enable or disable bear form dash. |
| `BleedingEdge` | string | `empty string` | Enable various weapon-specific changes; some are more experimental than others, see README for details. (Slashers, Crossbow, Pistols, TwinBlades, Daggers) |
| `PrimalArsenal` | bool | `False` | Experimental weapons with different models and abilities. |
| `PrimalJewelCost` | int (PrefabGUID) | `-77477508` | If extra recipes is enabled with a valid item prefab here (default demon fragments), it can be refined via gemcutter for random enhanced tier 4 jewels (better rolls, more modifiers). |

## StarterKit

| Setting | Type | Default | Description |
|---|---|---|---|
| `StarterKit` | bool | `False` | Enable or disable the starter kit. |
| `KitPrefabs` | string (PrefabGUID list) | `"862477668,-1531666018,-1593377811,1821405450"` | Item prefabGuids for starting kit. |
| `KitQuantities` | string | `"500,1000,1000,250"` | The quantity of each item in the starter kit. |
| `KitFamiliar` | int (PrefabGUID) | `0` | Character Prefab GUID for a familiar to grant with the starter kit (0 disables). |

Exact defaults are shown for stable 1.13.22. Only constraints explicitly stated in the descriptions should be treated as supported ranges.

---

[Wiki Home](../HOME.md) · [Commands](COMMANDS.md) · [Configuration](CONFIGURATION.md)
