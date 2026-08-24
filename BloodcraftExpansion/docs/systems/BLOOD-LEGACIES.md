# Blood Legacies

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

## What it is

Blood Legacies add persistent progression per blood type. Progress comes from feeding executions and completions. The stable source defines Worker, Warrior, Scholar, Rogue, Mutant, Draculin, Immortal, Creature, Brute, and Corruption legacies.

Each blood can hold up to `LegacyStatChoices` selected bonuses (default `3`). Their strength scales with blood legacy level, base caps, class synergies, and legacy prestige.

## Default stat caps

| Stat | Default cap |
|---|---:|
| HealingReceived | 0.15 |
| DamageReduction | 0.05 |
| PhysicalResistance | 0.10 |
| SpellResistance | 0.10 |
| ResourceYield | 0.25 |
| ReducedBloodDrain | 0.50 |
| SpellCooldownRecoveryRate | 0.10 |
| WeaponCooldownRecoveryRate | 0.10 |
| UltimateCooldownRecoveryRate | 0.20 |
| MinionDamage | 0.25 |
| AbilityAttackSpeed | 0.10 |
| CorruptionDamageReduction | 0.10 |

## Commands

- `.bl get [BloodType]` — inspect a legacy.
- `.bl l` — list blood legacies.
- `.bl lst` — list selectable stats.
- `.bl cst [BloodOrStat] [BloodStat]` — select a stat.
- `.bl rst` — reset choices for the configured cost.
- `.bl log` — toggle progress messages.

See [Legacy Stats](../progression/LEGACY-STATS.md), [Prestige Types](../progression/PRESTIGE-TYPES.md), and [legacy configuration](../reference/CONFIG-LEGACIES.md).
---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
