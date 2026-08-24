# V Blood Echoes

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

Primal Echoes provide a controlled endgame route to eligible V Blood familiar unlocks.

## Requirements

- `FamiliarSystem=true` and `PrimalEchoes=true`.
- `AllowVBloods=true` for the requested unit.
- Exo prestige enabled, which itself requires normal prestige progression.
- The unit must not be blocked by `BannedUnits` or `BannedTypes`.
- Enough configured Exo reward currency.

Use `.fam echoes [VBloodName]`. Cost scales with unit level/tier and then with `EchoesFactor` (default `1`, clamped to `1–4`). The stable description says the highest tier—shard bearers—costs 25 times `ExoPrestigeRewardQuantity`.

This command purchases an unlock; it does not bypass eligibility bans. See [Exo Prestige](../progression/EXO-PRESTIGE.md).
---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
