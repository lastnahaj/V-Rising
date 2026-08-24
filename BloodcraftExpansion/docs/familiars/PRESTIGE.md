# Familiar Prestige

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

Familiar prestige is optional and separate from player prestige.

## Requirements

- `FamiliarPrestige=true`.
- An active familiar.
- Either the configured maximum familiar level or enough Schematics for the shortcut.
- A prestige count below `MaxFamiliarPrestiges` (default `10`).

`.fam pr` resets an eligible familiar to level 1 and increments its prestige. `FamiliarPrestigeStatMultiplier` defaults to `0.10` and scales applicable familiar stats per prestige.

The Schematics shortcut uses `PrestigeCostItemQuantity`, default `1000`. Source clamps this value to `500–2000`. Extra levels beyond a paid prestige threshold can carry into the resulting familiar level.

Back up `BepInEx/config/Bloodcraft/Familiars/FamiliarLeveling/` before major changes to caps or prestige rules.
---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
