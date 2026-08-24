# Primal Jewels

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

When `ExtraRecipes` is enabled, the Gem Cutting Table can refine the item identified by `PrimalJewelCost` into a random enhanced tier 4 jewel with more modifiers and stronger rolls. The default input PrefabGUID represents a Demon Fragment in the upstream documentation.

Adding a perfect gem to the refinement can influence the resulting spell school. Daily and weekly quests can award perfect gems according to configured chances.

Because `PrimalJewelCost` is an integer PrefabGUID, administrators should preserve a verified value rather than assigning a guessed item ID.

See [Extra Recipes](EXTRA-RECIPES.md) for the recipe and [Quest Rewards](../quests/REWARDS.md) for the reward connection.

---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
