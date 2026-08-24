# Demon Hunter

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

**Wiki role summary:** Mobile physical critical pressure. This is an editorial summary, not an upstream role label.

## Mechanical identity

Its defaults emphasize speed, primary attacks, physical critical strikes, and Storm-school effects.

## Default synergies

| Layer | Stable default |
|---|---|
| Weapon stats | MovementSpeed,  PrimaryAttackSpeed,  PhysicalCritChance,  PhysicalCritDamage |
| Blood stats | PhysicalResistance,  ReducedBloodDrain,  WeaponCooldownRecoveryRate,  MinionDamage |
| On-hit theme | Storm: Static; an already affected target can grant Storm Charge to the attacker |

A useful equipment/blood setup is any build whose selected expertise and legacy stats overlap these configured synergy lists. Blood type and weapon category are not hard-locked by the class.

## Shift spell progression

All classes share Veil of Shadow as the default configured spell. The stable source then defines:

| Order | Ability group | PrefabGUID |
|---:|---|---:|
| 1 | Cyclone | `-356990326` |
| 2 | Polarity Shift | `-987810170` |
| 3 | Lightning Wall | `1071205195` |
| 4 | Ball Lightning | `1249925269` |
| 5 | Lightning Typhoon | `-914344112` |

These names come from identifiers in the stable source. Administrators may replace the PrefabGUID list, so use `.class lsp DemonHunter` to inspect the live server. Unknown replacement IDs should remain labeled as PrefabGUIDs rather than guessed.

## Commands and settings

- `.class lst DemonHunter` — show live synergy lists.
- `.class lsp DemonHunter` — show available class spells.
- `.class csp [#]` — choose an unlocked spell.
- `.class shift` — toggle the Shift slot.
- Relevant settings: `ClassSystem`, `ClassOnHitEffects`, `OnHitProcChance`, `SynergyMultiplier`, the class synergy keys, and the class spell key.

See [Classes Overview](README.md) and [Class Configuration](../reference/CONFIG-CLASSES.md).
---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
