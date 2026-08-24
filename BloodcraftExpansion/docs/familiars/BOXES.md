# Familiar Boxes

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

Boxes divide a player’s familiar collection into named sets. One box is active for listing and numbered binding.

| Command | Action |
|---|---|
| `.fam boxes` | List boxes. |
| `.fam cb [Name]` | Choose the active box. |
| `.fam ab [BoxName]` | Add an empty box. |
| `.fam rb [CurrentName] [NewName]` | Rename a box. |
| `.fam mb [BoxName]` | Move the active familiar to another box. |
| `.fam db [BoxName]` | Delete an empty box. |
| `.fam l` | List familiars in the active box. |
| `.fam s [Name]` | Search boxes. |
| `.fam sb [Name]` | Search and bind a unique match. |
| `.fam r [#]` | Permanently remove an unlock from the active set. |

Removal is destructive for that unlock. Unbinding with `.fam ub` is the normal way to dismiss an active familiar without deleting the collection entry.
---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
