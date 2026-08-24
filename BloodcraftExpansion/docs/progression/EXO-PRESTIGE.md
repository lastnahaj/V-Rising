# Exo Prestige

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

!!! experimental "Experimental Bloodcraft Feature"
    Exo forms are described upstream as potentially unbalanced and still carrying rough edges. Test them before production use.

## Prerequisites

`ExoPrestiging` requires the normal prestige system, which requires leveling. Stable configuration describes Exo prestige as available after normal prestiges are maxed, with 100 Exo prestiges.

## Reward loop

Each Exo prestige grants `ExoPrestigeRewardQuantity` of `ExoPrestigeReward`. Defaults are PrefabGUID `28358550` and quantity `500`. The wiki does not assign a friendly meaning beyond the upstream description (“tier 3 nether shards”) because servers may replace the PrefabGUID.

That currency also powers [Primal Echoes](../familiars/V-BLOOD-ECHOES.md) when configured.

## Exo form

- `.prestige exoform` toggles taunting to enter the selected form.
- `.prestige sf [EvolvedVampire|CorruptedSerpent]` chooses the active form.
- `TrueImmortal=true` enables Immortal blood for the form duration.

The upstream changelog describes both forms as potentially unbalanced with remaining rough edges. Keep the feature opt-in and verify transformation, cooldown, death, reconnect, and restart behavior on a test server.
---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
