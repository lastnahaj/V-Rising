# Legacy Stats

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

Players choose up to `LegacyStatChoices` stats per blood legacy, default `3`. Values scale with the blood’s level; class synergy and legacy prestige can modify the cap.

| Exact stat name | Stable default cap |
|---|---:|
| `HealingReceived` | 0.15 |
| `DamageReduction` | 0.05 |
| `PhysicalResistance` | 0.10 |
| `SpellResistance` | 0.10 |
| `ResourceYield` | 0.25 |
| `ReducedBloodDrain` | 0.50 |
| `SpellCooldownRecoveryRate` | 0.10 |
| `WeaponCooldownRecoveryRate` | 0.10 |
| `UltimateCooldownRecoveryRate` | 0.20 |
| `MinionDamage` | 0.25 |
| `AbilityAttackSpeed` | 0.10 |
| `CorruptionDamageReduction` | 0.10 |

Use `.bl lst` for the live list, `.bl cst [BloodOrStat] [BloodStat]` to choose, and `.bl rst` to clear the current blood’s choices for the configured cost.
---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
