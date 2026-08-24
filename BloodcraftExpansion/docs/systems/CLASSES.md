# Classes System

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

Bloodcraft 1.13.22 provides six upstream classes: Blood Knight, Demon Hunter, Vampire Lord, Shadow Blade, Arcane Sorcerer, and Death Mage.

A class can provide three mechanical layers:

1. **Weapon-stat synergies** multiply configured expertise stat caps.
2. **Blood-stat synergies** multiply configured legacy stat caps.
3. **Spell-school behavior** can apply an on-hit debuff at `OnHitProcChance` and unlock configured Shift spells through leveling prestige.

The default synergy multiplier is `1.5`. The on-hit chance defaults to `0.075`. Shift use also requires `ShiftSlot` and is controlled by `.class shift`.

## Selection

Initial selection is free. Later changes use the item and quantity configured by `ChangeClassItem` and `ChangeClassQuantity`.

| Command | Purpose |
|---|---|
| `.class l` | List classes. |
| `.class lst [Class]` | List the class’s weapon and blood synergies. |
| `.class lsp [Class]` | List configured spells. |
| `.class s [Class]` | Select an initial class. |
| `.class c [Class]` | Change class and pay the configured cost. |
| `.class csp [#]` | Choose an unlocked class spell. |
| `.class shift` | Lock or unlock the Shift spell. |

The names above must not be confused with the repository’s [custom proposed classes](../custom/CUSTOM-CLASSES.md). Continue to the [upstream class comparison](../classes/README.md).
---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
