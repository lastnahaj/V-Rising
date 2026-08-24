# Expertise Stats

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

Players choose up to `ExpertiseStatChoices` stats per weapon, default `3`. The value at maximum expertise is based on the configured cap; class synergy and expertise prestige can modify it.

| Exact stat name | Display family | Stable default cap |
|---|---|---:|
| `MaxHealth` | Integer | 250 |
| `MovementSpeed` | Decimal | 0.25 |
| `PrimaryAttackSpeed` | Percentage | 0.10 |
| `PhysicalLifeLeech` | Percentage | 0.10 |
| `SpellLifeLeech` | Percentage | 0.10 |
| `PrimaryLifeLeech` | Percentage | 0.15 |
| `PhysicalPower` | Integer | 20 |
| `SpellPower` | Integer | 10 |
| `PhysicalCritChance` | Percentage | 0.10 |
| `PhysicalCritDamage` | Percentage | 0.50 |
| `SpellCritChance` | Percentage | 0.10 |
| `SpellCritDamage` | Percentage | 0.50 |

Use `.wep lst` for the live list and `.wep cst [WeaponOrStat] [WeaponStat]` to choose. `.wep rst` clears the current weapon’s selections for the configured item cost.

A class synergy multiplies only matching configured stats; it does not require a specific weapon category. See [Upstream Classes](../classes/README.md).
---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
