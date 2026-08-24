# Weapon Expertise Configuration

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

## Expertise

| Setting | Type | Default | Description |
|---|---|---|---|
| `ExpertiseSystem` | bool | `False` | Enable or disable the expertise system. |
| `MaxExpertisePrestiges` | int | `10` | The maximum number of prestiges a player can reach in expertise. |
| `UnarmedSlots` | bool (PrefabGUID list) | `False` | Enable or disable extra spells while unarmed. |
| `FishingSlots` | bool (PrefabGUID list) | `False` | Enable or disable extra spells while fishing. |
| `Duality` | bool | `True` | True for both unarmed slots, false for one unarmed slot. Does nothing without UnarmedSlots enabled (doesn't apply to fishing pole). |
| `ShiftSlot` | bool | `False` | Enable or disable using class spell on shift. |
| `MaxExpertiseLevel` | int | `100` | The maximum level a player can reach in weapon expertise. |
| `UnitExpertiseMultiplier` | float | `2` | The multiplier for expertise gained from units. |
| `VBloodExpertiseMultiplier` | float | `5` | The multiplier for expertise gained from VBloods. |
| `UnitSpawnerExpertiseFactor` | float | `1` | The multiplier for experience gained from unit spawners (vermin nests, tombs). |
| `ExpertiseStatChoices` | int | `3` | The maximum number of stat choices a player can pick for a weapon expertise. Max of 3 will be sent to client UI for display. |
| `ResetExpertiseItem` | int (PrefabGUID) | `576389135` | Item PrefabGUID cost for resetting weapon stats. |
| `ResetExpertiseItemQuantity` | int | `500` | Quantity of item required for resetting stats. |
| `MaxHealth` | float | `250` | The base cap for maximum health. |
| `MovementSpeed` | float | `0.25` | The base cap for movement speed. |
| `PrimaryAttackSpeed` | float | `0.1` | The base cap for primary attack speed. |
| `PhysicalLifeLeech` | float | `0.1` | The base cap for physical life leech. |
| `SpellLifeLeech` | float | `0.1` | The base cap for spell life leech. |
| `PrimaryLifeLeech` | float | `0.15` | The base cap for primary life leech. |
| `PhysicalPower` | float | `20` | The base cap for physical power. |
| `SpellPower` | float | `10` | The base cap for spell power. |
| `PhysicalCritChance` | float | `0.1` | The base cap for physical critical strike chance. |
| `PhysicalCritDamage` | float | `0.5` | The base cap for physical critical strike damage. |
| `SpellCritChance` | float | `0.1` | The base cap for spell critical strike chance. |
| `SpellCritDamage` | float | `0.5` | The base cap for spell critical strike damage. |

Exact defaults are shown for stable 1.13.22. Only constraints explicitly stated in the descriptions should be treated as supported ranges.

---

[Wiki Home](../HOME.md) · [Commands](COMMANDS.md) · [Configuration](CONFIGURATION.md)
