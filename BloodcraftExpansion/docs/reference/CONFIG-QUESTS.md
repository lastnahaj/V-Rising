# Quests Configuration

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

## Quests

| Setting | Type | Default | Description |
|---|---|---|---|
| `QuestSystem` | bool | `False` | Enable or disable quests (kill, gather, and crafting). |
| `InfiniteDailies` | bool | `False` | Enable or disable infinite dailies. |
| `DailyPerfectChance` | float | `0.1` | Chance to receive a random perfect gem (can be used to control spell school for primal jewels in gemcutter) when completing daily quests. |
| `QuestRewards` | string (PrefabGUID list) | `"28358550,576389135,-257494203"` | Item prefabs for quest reward pool. |
| `QuestRewardAmounts` | string | `"50,250,50"` | The amount of each reward in the pool. Will be multiplied accordingly for weeklies (*5) and vblood kill quests (*3). |
| `RerollDailyPrefab` | int (PrefabGUID) | `-949672483` | Prefab item for rerolling daily. |
| `RerollDailyAmount` | int (PrefabGUID) | `50` | Cost of prefab for rerolling daily. |
| `RerollWeeklyPrefab` | int (PrefabGUID) | `-949672483` | Prefab item for rerolling weekly. |
| `RerollWeeklyAmount` | int (PrefabGUID) | `50` | Cost of prefab for rerolling weekly. Won't work if already completed for the week. |

Exact defaults are shown for stable 1.13.22. Only constraints explicitly stated in the descriptions should be treated as supported ranges.

---

[Wiki Home](../HOME.md) · [Commands](COMMANDS.md) · [Configuration](CONFIGURATION.md)
