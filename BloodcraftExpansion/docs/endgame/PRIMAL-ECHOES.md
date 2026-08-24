# Primal Echoes

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

**Status:** Optional Exo Prestige and Familiar integration.

Primal Echoes let a player spend the configured Exo prestige reward to purchase an eligible V Blood familiar unlock using `.fam echoes [VBloodName]`.

## Required systems

- Leveling and Prestige;
- Exo Prestige;
- Familiars;
- `PrimalEchoes=true`;
- `AllowVBloods=true`;
- an unbanned requested unit.

Costs scale by V Blood tier from `ExoPrestigeRewardQuantity` and then by `EchoesFactor` (1–4 after clamping). Stable documentation identifies shard bearers as the highest tier at 25 times the base quantity.

See [V Blood Echoes](../familiars/V-BLOOD-ECHOES.md) for the player flow.
---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
