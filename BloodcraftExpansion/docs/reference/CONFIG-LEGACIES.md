# Blood Legacies Configuration

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

## Legacies

| Setting | Type | Default | Description |
|---|---|---|---|
| `LegacySystem` | bool | `False` | Enable or disable the blood legacy system. |
| `MaxLegacyPrestiges` | int | `10` | The maximum number of prestiges a player can reach in blood legacies. |
| `MaxBloodLevel` | int | `100` | The maximum level a player can reach in blood legacies. |
| `UnitLegacyMultiplier` | float | `1` | The multiplier for lineage gained from units. |
| `VBloodLegacyMultiplier` | float | `5` | The multiplier for lineage gained from VBloods. |
| `LegacyStatChoices` | int | `3` | The maximum number of stat choices a player can pick for a blood legacy. Max of 3 will be sent to client UI for display. |
| `ResetLegacyItem` | int (PrefabGUID) | `576389135` | Item PrefabGUID cost for resetting blood stats. |
| `ResetLegacyItemQuantity` | int | `500` | Quantity of item required for resetting blood stats. |
| `HealingReceived` | float | `0.15` | The base cap for healing received. |
| `DamageReduction` | float | `0.05` | The base cap for damage reduction. |
| `PhysicalResistance` | float | `0.1` | The base cap for physical resistance. |
| `SpellResistance` | float | `0.1` | The base cap for spell resistance. |
| `ResourceYield` | float | `0.25` | The base cap for resource yield. |
| `ReducedBloodDrain` | float | `0.5` | The base cap for reduced blood drain. |
| `SpellCooldownRecoveryRate` | float | `0.1` | The base cap for spell cooldown recovery rate. |
| `WeaponCooldownRecoveryRate` | float | `0.1` | The base cap for weapon cooldown recovery rate. |
| `UltimateCooldownRecoveryRate` | float | `0.2` | The base cap for ultimate cooldown recovery rate. |
| `MinionDamage` | float | `0.25` | The base cap for minion damage. |
| `AbilityAttackSpeed` | float | `0.1` | The base cap for ability attack speed. |
| `CorruptionDamageReduction` | float | `0.1` | The base cap for corruption damage reduction. |

Exact defaults are shown for stable 1.13.22. Only constraints explicitly stated in the descriptions should be treated as supported ranges.

---

[Wiki Home](../HOME.md) · [Commands](COMMANDS.md) · [Configuration](CONFIGURATION.md)
