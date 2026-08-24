# Vampire Lord

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

**Wiki role summary:** Hybrid durability and Frost spell pressure. This is an editorial summary, not an upstream role label.

## Mechanical identity

Its defaults mix health, physical and spell power, spell leech, defensive blood bonuses, and Frost-school effects.

## Default synergies

| Layer | Stable default |
|---|---|
| Weapon stats | MaxHealth,  SpellLifeLeech,  PhysicalPower,  SpellPower |
| Blood stats | DamageReduction,  SpellResistance,  UltimateCooldownRecoveryRate,  CorruptionDamageReduction |
| On-hit theme | Frost: Chill; an already affected target can grant Frost Weapon to the attacker |

A useful equipment/blood setup is any build whose selected expertise and legacy stats overlap these configured synergy lists. Blood type and weapon category are not hard-locked by the class.

## Shift spell progression

All classes share Veil of Shadow as the default configured spell. The stable source then defines:

| Order | Ability group | PrefabGUID |
|---:|---|---:|
| 1 | Frost Bat | `78384915` |
| 2 | Crystal Lance | `295045820` |
| 3 | Cold Snap | `-1000260252` |
| 4 | Ice Nova | `91249849` |
| 5 | Arctic Leap | `1966330719` |

These names come from identifiers in the stable source. Administrators may replace the PrefabGUID list, so use `.class lsp VampireLord` to inspect the live server. Unknown replacement IDs should remain labeled as PrefabGUIDs rather than guessed.

## Commands and settings

- `.class lst VampireLord` — show live synergy lists.
- `.class lsp VampireLord` — show available class spells.
- `.class csp [#]` — choose an unlocked spell.
- `.class shift` — toggle the Shift slot.
- Relevant settings: `ClassSystem`, `ClassOnHitEffects`, `OnHitProcChance`, `SynergyMultiplier`, the class synergy keys, and the class spell key.

See [Classes Overview](README.md) and [Class Configuration](../reference/CONFIG-CLASSES.md).
---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
