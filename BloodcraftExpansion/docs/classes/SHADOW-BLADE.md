# Shadow Blade

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

**Wiki role summary:** Fast weapon pressure with Chaos effects. This is an editorial summary, not an upstream role label.

## Mechanical identity

Its defaults emphasize movement, fast physical attacks, critical damage, weapon cooldowns, and Chaos-school effects.

## Default synergies

| Layer | Stable default |
|---|---|
| Weapon stats | MovementSpeed,  PrimaryAttackSpeed,  PhysicalPower,  PhysicalCritDamage |
| Blood stats | SpellResistance,  ReducedBloodDrain,  WeaponCooldownRecoveryRate,  AbilityAttackSpeed |
| On-hit theme | Chaos: Ignite; an already affected target can grant a Chaos-heated buff to the attacker |

A useful equipment/blood setup is any build whose selected expertise and legacy stats overlap these configured synergy lists. Blood type and weapon category are not hard-locked by the class.

## Shift spell progression

All classes share Veil of Shadow as the default configured spell. The stable source then defines:

| Order | Ability group | PrefabGUID |
|---:|---|---:|
| 1 | Chaos Volley | `1019568127` |
| 2 | Aftershock | `1575317901` |
| 3 | Power Surge | `1112116762` |
| 4 | Void | `-358319417` |
| 5 | Chaos Barrage | `1174831223` |

These names come from identifiers in the stable source. Administrators may replace the PrefabGUID list, so use `.class lsp ShadowBlade` to inspect the live server. Unknown replacement IDs should remain labeled as PrefabGUIDs rather than guessed.

## Commands and settings

- `.class lst ShadowBlade` — show live synergy lists.
- `.class lsp ShadowBlade` — show available class spells.
- `.class csp [#]` — choose an unlocked spell.
- `.class shift` — toggle the Shift slot.
- Relevant settings: `ClassSystem`, `ClassOnHitEffects`, `OnHitProcChance`, `SynergyMultiplier`, the class synergy keys, and the class spell key.

See [Classes Overview](README.md) and [Class Configuration](../reference/CONFIG-CLASSES.md).
---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
