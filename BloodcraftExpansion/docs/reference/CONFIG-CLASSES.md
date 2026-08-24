# Classes Configuration

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

## Classes

| Setting | Type | Default | Description |
|---|---|---|---|
| `ClassSystem` | bool | `False` | Enable classes without synergy restrictions. |
| `ChangeClassItem` | int (PrefabGUID) | `576389135` | Item PrefabGUID cost for changing class. |
| `ChangeClassQuantity` | int | `750` | Quantity of item required for changing class. |
| `ClassOnHitEffects` | bool | `True` | Enable or disable class spell school on hit effects (chance to proc respective debuff from spell school when dealing damage (leech, chill, condemn etc), second tier effect will proc if first is already present on target. |
| `OnHitProcChance` | float | `0.075` | The chance for a class effect to proc on hit. |
| `SynergyMultiplier` | float | `1.5` | Multiplier for class stat synergies to base stat cap. |
| `BloodKnightWeaponSynergies` | string | `"MaxHealth,PrimaryAttackSpeed,PrimaryLifeLeech,PhysicalPower"` | Blood Knight weapon synergies. |
| `BloodKnightBloodSynergies` | string | `"DamageReduction,ReducedBloodDrain,WeaponCooldownRecoveryRate,AbilityAttackSpeed"` | Blood Knight blood synergies. |
| `DemonHunterWeaponSynergies` | string | `"MovementSpeed,PrimaryAttackSpeed,PhysicalCritChance,PhysicalCritDamage"` | Demon Hunter weapon synergies. |
| `DemonHunterBloodSynergies` | string | `"PhysicalResistance,ReducedBloodDrain,WeaponCooldownRecoveryRate,MinionDamage"` | Demon Hunter blood synergies |
| `VampireLordWeaponSynergies` | string | `"MaxHealth,SpellLifeLeech,PhysicalPower,SpellPower"` | Vampire Lord weapon synergies. |
| `VampireLordBloodSynergies` | string | `"DamageReduction,SpellResistance,UltimateCooldownRecoveryRate,CorruptionDamageReduction"` | Vampire Lord blood synergies. |
| `ShadowBladeWeaponSynergies` | string | `"MovementSpeed,PrimaryAttackSpeed,PhysicalPower,PhysicalCritDamage"` | Shadow Blade weapon synergies. |
| `ShadowBladeBloodSynergies` | string | `"SpellResistance,ReducedBloodDrain,WeaponCooldownRecoveryRate,AbilityAttackSpeed"` | Shadow Blade blood synergies. |
| `ArcaneSorcererWeaponSynergies` | string | `"SpellLifeLeech,SpellPower,SpellCritChance,SpellCritDamage"` | Arcane Sorcerer weapon synergies. |
| `ArcaneSorcererBloodSynergies` | string | `"HealingReceived,SpellCooldownRecoveryRate,UltimateCooldownRecoveryRate,AbilityAttackSpeed"` | Arcane Sorcerer blood synergies. |
| `DeathMageWeaponSynergies` | string | `"MaxHealth,SpellLifeLeech,SpellPower,SpellCritDamage"` | Death Mage weapon synergies. |
| `DeathMageBloodSynergies` | string | `"PhysicalResistance,SpellResistance,SpellCooldownRecoveryRate,MinionDamage"` | Death Mage blood synergies. |
| `DefaultClassSpell` | int | `-433204738` | Default spell (veil of shadow) available to all classes. |
| `BloodKnightSpells` | string (PrefabGUID list) | `"-880131926,651613264,2067760264,189403977,375131842"` | Blood Knight shift spells, granted at levels of prestige. |
| `DemonHunterSpells` | string (PrefabGUID list) | `"-356990326,-987810170,1071205195,1249925269,-914344112"` | Demon Hunter shift spells, granted at levels of prestige. |
| `VampireLordSpells` | string (PrefabGUID list) | `"78384915,295045820,-1000260252,91249849,1966330719"` | Vampire Lord shift spells, granted at levels of prestige. |
| `ShadowBladeSpells` | string (PrefabGUID list) | `"1019568127,1575317901,1112116762,-358319417,1174831223"` | Shadow Blade shift spells, granted at levels of prestige. |
| `ArcaneSorcererSpells` | string (PrefabGUID list) | `"247896794,268059675,-242769430,-2053450457,1650878435"` | Arcane Sorcerer shift spells, granted at levels of prestige. |
| `DeathMageSpells` | string (PrefabGUID list) | `"-1204819086,481411985,1961570821,2138402840,-1781779733"` | Death Mage shift spells, granted at levels of prestige. |

Exact defaults are shown for stable 1.13.22. Only constraints explicitly stated in the descriptions should be treated as supported ranges.

---

[Wiki Home](../HOME.md) · [Commands](COMMANDS.md) · [Configuration](CONFIGURATION.md)
