# Weapon Expertise

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

## What it is

Weapon Expertise tracks progression separately for each supported weapon. The equipped weapon that delivers the final blow receives expertise. Bloodcraft 1.13.22 defines 16 types: Sword, Axe, Mace, Spear, Crossbow, GreatSword, Slashers, Pistols, Reaper, Longbow, Whip, Unarmed, FishingPole, TwinBlades, Daggers, and Claws.

Players choose up to `ExpertiseStatChoices` bonus stats per weapon (default `3`). Bonus strength scales with that weapon’s expertise level, the configured base cap, class synergy, and expertise prestige.

## Default stat caps

| Stat | Default cap |
|---|---:|
| MaxHealth | 250 |
| MovementSpeed | 0.25 |
| PrimaryAttackSpeed | 0.10 |
| PhysicalLifeLeech | 0.10 |
| SpellLifeLeech | 0.10 |
| PrimaryLifeLeech | 0.15 |
| PhysicalPower | 20 |
| SpellPower | 10 |
| PhysicalCritChance | 0.10 |
| PhysicalCritDamage | 0.50 |
| SpellCritChance | 0.10 |
| SpellCritDamage | 0.50 |

These are source-defined base caps, not percentages in every case. Use Eclipse or in-game output for display formatting.

## Commands

- `.wep get` — current weapon progress and choices.
- `.wep l` — available expertise types.
- `.wep lst` — available stats.
- `.wep cst [WeaponOrStat] [WeaponStat]` — choose a stat.
- `.wep rst` — reset the current weapon’s choices for the configured item cost.
- `.wep log` — toggle gain messages.

See [Expertise Stats](../progression/EXPERTISE-STATS.md), [weapon prestige](../weapons/EXPERTISE.md), and [expertise configuration](../reference/CONFIG-EXPERTISE.md).
---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
