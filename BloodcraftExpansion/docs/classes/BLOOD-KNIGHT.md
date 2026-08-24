# Blood Knight

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

**Wiki role summary:** Durable physical sustain. This is an editorial summary, not an upstream role label.

## Mechanical identity

Its defaults emphasize health, physical pressure, primary-attack sustain, defensive blood bonuses, and Blood-school effects.

## Default synergies

| Layer | Stable default |
|---|---|
| Weapon stats | MaxHealth,  PrimaryAttackSpeed,  PrimaryLifeLeech,  PhysicalPower |
| Blood stats | DamageReduction,  ReducedBloodDrain,  WeaponCooldownRecoveryRate,  AbilityAttackSpeed |
| On-hit theme | Blood: Leech; an already affected target can receive Blood Curse |

A useful equipment/blood setup is any build whose selected expertise and legacy stats overlap these configured synergy lists. Blood type and weapon category are not hard-locked by the class.

## Shift spell progression

All classes share Veil of Shadow as the default configured spell. The stable source then defines:

| Order | Ability group | PrefabGUID |
|---:|---|---:|
| 1 | Shadowbolt | `-880131926` |
| 2 | Blood Rage | `651613264` |
| 3 | Blood Fountain | `2067760264` |
| 4 | Sanguine Coil | `189403977` |
| 5 | Crimson Beam | `375131842` |

These names come from identifiers in the stable source. Administrators may replace the PrefabGUID list, so use `.class lsp BloodKnight` to inspect the live server. Unknown replacement IDs should remain labeled as PrefabGUIDs rather than guessed.

## Commands and settings

- `.class lst BloodKnight` — show live synergy lists.
- `.class lsp BloodKnight` — show available class spells.
- `.class csp [#]` — choose an unlocked spell.
- `.class shift` — toggle the Shift slot.
- Relevant settings: `ClassSystem`, `ClassOnHitEffects`, `OnHitProcChance`, `SynergyMultiplier`, the class synergy keys, and the class spell key.

See [Classes Overview](README.md) and [Class Configuration](../reference/CONFIG-CLASSES.md).
---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
