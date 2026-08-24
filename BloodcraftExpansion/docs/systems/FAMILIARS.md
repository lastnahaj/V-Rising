# Familiars System

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

Familiars turn eligible defeated enemies into persistent unlocks that players can bind as companions. The system covers unlock chance, boxes, search, summoning, combat, leveling, optional prestige, shiny spell-school effects, equipment, emote actions, V Blood eligibility, Primal Echoes, and an experimental battle queue.

## Core loop

1. Defeat eligible units for an unlock roll.
2. List the active box with `.fam l`.
3. Bind with `.fam b [#]` or search and bind with `.fam sb [Name]`.
4. Gain familiar experience alongside combat.
5. Inspect progress with `.fam gl`.
6. Optionally prestige at the configured maximum level or by paying Schematics.

Eligibility is controlled by `AllowVBloods`, `AllowMinions`, `BannedUnits`, and `BannedTypes`. Share behavior uses `ShareUnlocks` and leveling share distance. Server owners should review PvP behavior before launch.

!!! experimental "Experimental Bloodcraft Feature"
    This feature is opt-in or carries an upstream WIP/experimental warning. Test it before using it on a production server.

The upstream configuration says Familiar Battles are most likely not working after V Rising 1.1 and should be used at your own risk. Battle groups can still be managed, but the battle feature must not be presented as production-ready.

Open the [full familiar hub](../familiars/README.md) or [familiar configuration](../reference/CONFIG-FAMILIARS.md).
---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
