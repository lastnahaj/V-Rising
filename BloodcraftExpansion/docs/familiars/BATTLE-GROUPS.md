# Familiar Battle Groups

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

!!! experimental "Experimental Bloodcraft Feature"
    Upstream warns that Familiar Battles are most likely not working after V Rising 1.1. Use at your own risk and test away from production data.

Battle groups store familiar selections for the battle subsystem. Their management commands are present even though upstream warns the actual battle system is not reliable after V Rising 1.1.

| Command | Action |
|---|---|
| `.fam bgs` | List battle groups. |
| `.fam bg [BattleGroup]` | Show one group or the active group. |
| `.fam abg [BattleGroup]` | Create a group. |
| `.fam cbg [BattleGroup]` | Set the active group. |
| `.fam sbg [BattleGroupOrSlot] [Slot]` | Assign the active familiar to a slot. |
| `.fam dbg [BattleGroup]` | Delete a group. |
| `.fam challenge [PlayerName]` | Challenge or inspect queue details. |
| `.fam sba` | Admin: set the single arena center. |

Keep `FamiliarBattles=false` on production servers unless a current test confirms the full queue, arena, and resolution flow.
---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
