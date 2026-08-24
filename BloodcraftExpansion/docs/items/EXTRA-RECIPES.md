# Extra Recipes

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

The optional `ExtraRecipes` setting adds salvage conversions and refinement recipes. It also provides the normal crafting path for [Vampiric Dust](VAMPIRIC-DUST.md), which familiars use for shiny effects.

## Salvageable

| Item | Time | Salvage output |
|---|---:|---|
| EMP | 20 seconds | 2 Depleted Batteries; 15 Tech Scrap |
| Bat Hide | 15 seconds | 3 Lesser Stygian Shards; 5 Blood Essence |
| Copper Wires | 15 seconds | 1 Electricity |
| Primal Blood Essence | 10 seconds | 5 Electricity |
| Gold Ore | 10 seconds | 2 Gold Jewelry |
| Radiant Fiber | 10 seconds | 8 Gem Dust; 16 Plant Fiber; 24 Pollen |

## Refinable

| Output | Inputs | Station | Purpose |
|---|---|---|---|
| Primal Jewel | 1 configured item; Demon Fragment by default | Gem Cutting Table | Random enhanced tier 4 jewel; an accompanying perfect gem can influence spell school |
| Primal Stygian Shard | 8 Greater Stygian Shards | Gem Cutting Table | Default Primal Echo currency |
| Charged Battery | 1 Depleted Battery; 1 Electricity | Fabricator | Battery conversion |
| 100 Blood Crystal | 100 Crystals; 1 Greater Blood Essence | Advanced Blood Press | Blood Crystal production |
| Copper Wires | 3 Copper Ingots | Fabricator | Wire production |
| Vampiric Dust | 8 Bleeding Hearts; 40 Blood Crystals | Advanced Grinder | Apply or change familiar shiny effects |

The Primal Jewel input is controlled by the `PrimalJewelCost` PrefabGUID. The other listed quantities are the stable 1.13.22 definitions.

---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
