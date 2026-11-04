# Bloodcraft Currencies

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.26 stable<br>
    **Last verified:** 2026-11-02<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

Several systems consume configured item PrefabGUIDs rather than a universal Bloodcraft currency.

| Use | Configuration | Stable default meaning documented upstream |
|---|---|---|
| Daily quest reroll | `RerollDailyPrefab`, `RerollDailyAmount` | Configured item and quantity |
| Weekly quest reroll | `RerollWeeklyPrefab`, `RerollWeeklyAmount` | Configured item and quantity |
| Familiar shiny | `ShinyCostItemQuantity` | Vampiric Dust quantity |
| Primal Echo | `ExoPrestigeReward`, `ExoPrestigeRewardQuantity`, `EchoesFactor` | Exo reward item and tier-scaled quantity; upstream defaults describe tier 3 nether shards |
| Primal Jewel | `PrimalJewelCost` | Configured refinement input; Demon Fragment by default |

The numeric values are PrefabGUIDs. This wiki does not assign unverified friendly names to them.

---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
