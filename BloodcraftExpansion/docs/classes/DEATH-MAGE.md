# Death Mage

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

**Wiki role summary:** Spell sustain and minion-oriented Unholy pressure. This is an editorial summary, not an upstream role label.

## Mechanical identity

Its defaults combine health, spell sustain, spell critical damage, resistance, minion damage, and Unholy-school effects.

## Default synergies

| Layer | Stable default |
|---|---|
| Weapon stats | MaxHealth,  SpellLifeLeech,  SpellPower,  SpellCritDamage |
| Blood stats | PhysicalResistance,  SpellResistance,  SpellCooldownRecoveryRate,  MinionDamage |
| On-hit theme | Unholy: Condemn; an already affected target can receive Unholy Amplify |

A useful equipment/blood setup is any build whose selected expertise and legacy stats overlap these configured synergy lists. Blood type and weapon category are not hard-locked by the class.

## Shift spell progression

All classes share Veil of Shadow as the default configured spell. The stable source then defines:

| Order | Ability group | PrefabGUID |
|---:|---|---:|
| 1 | Corrupted Skull | `-1204819086` |
| 2 | Corpse Explosion | `481411985` |
| 3 | Death Knight | `1961570821` |
| 4 | Soulburn | `2138402840` |
| 5 | Army of the Dead | `-1781779733` |

These names come from identifiers in the stable source. Administrators may replace the PrefabGUID list, so use `.class lsp DeathMage` to inspect the live server. Unknown replacement IDs should remain labeled as PrefabGUIDs rather than guessed.

## Commands and settings

- `.class lst DeathMage` — show live synergy lists.
- `.class lsp DeathMage` — show available class spells.
- `.class csp [#]` — choose an unlocked spell.
- `.class shift` — toggle the Shift slot.
- Relevant settings: `ClassSystem`, `ClassOnHitEffects`, `OnHitProcChance`, `SynergyMultiplier`, the class synergy keys, and the class spell key.

See [Classes Overview](README.md) and [Class Configuration](../reference/CONFIG-CLASSES.md).
---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
