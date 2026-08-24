# Professions

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

Bloodcraft provides eight gathering and crafting professions. `ProfessionSystem` enables the feature, `ProfessionFactor` multiplies XP, and `DisabledProfessions` can disable named professions as a comma-separated list.

| Profession | Main activity | Documented benefit |
|---|---|---|
| [Mining](MINING.md) | Breaking mineral-related resource objects and handling recognized mineral, stone, blood-crystal, tech-scrap, or emery items. | Bonus resources scale with profession level; upstream also documents gold ore as a profession-specific drop that can be salvaged into jewelry. |
| [Woodcutting](WOODCUTTING.md) | Breaking wood resource objects and recognized wood items. | Bonus resources scale with level, with random saplings as the documented profession-specific bonus. |
| [Harvesting](HARVESTING.md) | Gathering vegetation and recognized plant or trippy-shroom items. | Bonus gathered resources scale with level, with random seeds as the documented profession-specific bonus. |
| [Fishing](FISHING.md) | Successful fishing and recognized fish items. | Bloodcraft documents an additional random fish from the relevant location every 20 profession levels. |
| [Alchemy](ALCHEMY.md) | Recognized potions, bottles, flasks, consumables, blood potions/merlots, elixirs, coatings, and related crafting. | Potion effectiveness and duration scale up to twice baseline at maximum. Holy potion effectiveness is excluded, though duration can increase; coating duration is intentionally not extended. |
| [Blacksmithing](BLACKSMITHING.md) | Weapon-related crafting and recognized weapon items. | Upstream documents crafted gear base-stat improvement up to 10% and durability up to twice baseline at maximum for the relevant equipment category. |
| [Enchanting](ENCHANTING.md) | Recognized gems, jewels, magic sources, and related crafting. | It belongs to the crafted-equipment profession group that can improve relevant base stats up to 10% and durability up to twice baseline at maximum. |
| [Tailoring](TAILORING.md) | Recognized armor, cloaks, bags, cloth, and clothing-piece crafting. | Upstream documents relevant crafted gear base-stat improvement up to 10% and durability up to twice baseline at maximum. |

## Commands

- `.prof l` — list active professions.
- `.prof get [Profession]` — show progress.
- `.prof log` — toggle gain messages.
- `.prof set [Name] [Profession] [Level]` — administrator level override.

The source exposes one global XP multiplier rather than separate per-profession rates. Disable an unwanted profession by exact enum name: `Enchanting`, `Alchemy`, `Harvesting`, `Blacksmithing`, `Tailoring`, `Woodcutting`, `Mining`, or `Fishing`.
---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
