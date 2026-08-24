# Exo Forms

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

!!! experimental "Experimental Bloodcraft Feature"
    This opt-in system has an upstream experimental, WIP, or known-issue signal. Validate it in a test environment.

Exo forms are rewards tied to [Exo Prestige](../progression/EXO-PRESTIGE.md). Stable commands expose two choices:

- `EvolvedVampire`
- `CorruptedSerpent`

Use `.prestige sf [EvolvedVampire|CorruptedSerpent]` to select, then `.prestige exoform` to toggle taunt-based entry. Both forms share the configured duration/cooldown behavior. `TrueImmortal` optionally provides Immortal blood for the duration.

Upstream calls these forms potentially unbalanced and notes remaining rough behavior. Test death, disconnect, shapeshift transitions, PvP, castle boundaries, and cooldown persistence.
---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
