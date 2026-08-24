# Upstream Bloodcraft Classes

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

Classes are configurable mechanical packages, not fixed MMO roles. The role labels below are **wiki summaries** derived from stable default synergies and spell schools; Bloodcraft itself defines the exact stats and effects.

| Class | Wiki role summary | Weapon synergies | Blood synergies | Spell theme |
|---|---|---|---|---|
| [Blood Knight](BLOOD-KNIGHT.md) | Durable physical sustain | MaxHealth,  PrimaryAttackSpeed,  PrimaryLifeLeech,  PhysicalPower | DamageReduction,  ReducedBloodDrain,  WeaponCooldownRecoveryRate,  AbilityAttackSpeed | Blood |
| [Demon Hunter](DEMON-HUNTER.md) | Mobile physical critical pressure | MovementSpeed,  PrimaryAttackSpeed,  PhysicalCritChance,  PhysicalCritDamage | PhysicalResistance,  ReducedBloodDrain,  WeaponCooldownRecoveryRate,  MinionDamage | Storm |
| [Vampire Lord](VAMPIRE-LORD.md) | Hybrid durability and Frost spell pressure | MaxHealth,  SpellLifeLeech,  PhysicalPower,  SpellPower | DamageReduction,  SpellResistance,  UltimateCooldownRecoveryRate,  CorruptionDamageReduction | Frost |
| [Shadow Blade](SHADOW-BLADE.md) | Fast weapon pressure with Chaos effects | MovementSpeed,  PrimaryAttackSpeed,  PhysicalPower,  PhysicalCritDamage | SpellResistance,  ReducedBloodDrain,  WeaponCooldownRecoveryRate,  AbilityAttackSpeed | Chaos |
| [Arcane Sorcerer](ARCANE-SORCERER.md) | Spell critical pressure and Illusion utility | SpellLifeLeech,  SpellPower,  SpellCritChance,  SpellCritDamage | HealingReceived,  SpellCooldownRecoveryRate,  UltimateCooldownRecoveryRate,  AbilityAttackSpeed | Illusion |
| [Death Mage](DEATH-MAGE.md) | Spell sustain and minion-oriented Unholy pressure | MaxHealth,  SpellLifeLeech,  SpellPower,  SpellCritDamage | PhysicalResistance,  SpellResistance,  SpellCooldownRecoveryRate,  MinionDamage | Unholy |

## Shared rules

- Initial selection uses `.class s [Class]`; later changes use `.class c [Class]` and the configured cost.
- `SynergyMultiplier` defaults to `1.5` and multiplies matching stat caps.
- `ClassOnHitEffects` and `OnHitProcChance` control themed effects.
- Every class receives the configured default spell, Veil of Shadow (`PrefabGUID -433204738`).
- Five class-specific Shift spells are configured per class and unlocked at `PrestigeLevelsToUnlockClassSpells`.
- `.class lsp [Class]` resolves the active server configuration; it is the best in-game source after an administrator changes spell lists.

These six upstream classes are separate from the [ten proposed BloodcraftExpansion classes](../custom/CUSTOM-CLASSES.md).
---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
