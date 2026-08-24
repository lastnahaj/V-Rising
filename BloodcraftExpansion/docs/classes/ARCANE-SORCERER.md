# Arcane Sorcerer

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

**Wiki role summary:** Spell critical pressure and Illusion utility. This is an editorial summary, not an upstream role label.

## Mechanical identity

Its defaults concentrate on spell power, spell criticals, recovery, healing received, and Illusion-school effects.

## Default synergies

| Layer | Stable default |
|---|---|
| Weapon stats | SpellLifeLeech,  SpellPower,  SpellCritChance,  SpellCritDamage |
| Blood stats | HealingReceived,  SpellCooldownRecoveryRate,  UltimateCooldownRecoveryRate,  AbilityAttackSpeed |
| On-hit theme | Illusion: Weaken; an already affected target can grant Illusion Shield to the attacker |

A useful equipment/blood setup is any build whose selected expertise and legacy stats overlap these configured synergy lists. Blood type and weapon category are not hard-locked by the class.

## Shift spell progression

All classes share Veil of Shadow as the default configured spell. The stable source then defines:

| Order | Ability group | PrefabGUID |
|---:|---|---:|
| 1 | Spectral Wolf | `247896794` |
| 2 | Mosquito | `268059675` |
| 3 | Wraith Spear | `-242769430` |
| 4 | Phantom Aegis | `-2053450457` |
| 5 | Spectral Guardian | `1650878435` |

These names come from identifiers in the stable source. Administrators may replace the PrefabGUID list, so use `.class lsp ArcaneSorcerer` to inspect the live server. Unknown replacement IDs should remain labeled as PrefabGUIDs rather than guessed.

## Commands and settings

- `.class lst ArcaneSorcerer` — show live synergy lists.
- `.class lsp ArcaneSorcerer` — show available class spells.
- `.class csp [#]` — choose an unlocked spell.
- `.class shift` — toggle the Shift slot.
- Relevant settings: `ClassSystem`, `ClassOnHitEffects`, `OnHitProcChance`, `SynergyMultiplier`, the class synergy keys, and the class spell key.

See [Classes Overview](README.md) and [Class Configuration](../reference/CONFIG-CLASSES.md).
---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
