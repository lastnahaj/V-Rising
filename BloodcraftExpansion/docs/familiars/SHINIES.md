# Shiny Familiars

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

A shiny is a familiar-specific spell-school effect. Stable Bloodcraft uses six themed effects and gives the familiar a chance to apply the corresponding school debuff while dealing damage.

## Unlock behavior

- `ShinyChance` defaults to `0.2` (20%) on the first unlock.
- A repeated unlock of the same unit guarantees a shiny when the configured chance is greater than zero.
- On-hit chance uses the configured class `OnHitProcChance`.

## Choosing or changing a shiny

Use `.fam shiny [SpellSchool]` with an active familiar. The cost is Vampiric Dust controlled by `ShinyCostItemQuantity` (default `100`, clamped to `50–200`). Changing an existing shiny costs 25% of that value.

The stable spell-school set follows Blood, Chaos, Storm, Illusion, Frost, and Unholy effects. Use in-game command feedback for accepted spell-school spelling.

Vampiric Dust is craftable only when `ExtraRecipes` is enabled unless the server supplies it another way. See [Vampiric Dust](../items/VAMPIRIC-DUST.md).
---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
