# Familiar Administration

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

## High-impact settings

Review unlock chances, V Blood/minion eligibility, category bans, PvP participation, maximum level, prestige, shiny costs, inventory mode, sharing, and combat before opening the system to players.

## Equipment and emotes

Players enable emote actions with `.fam e` and list them with `.fam actions`. Beckon opens or closes the familiar equipment interaction. `EquipmentOnly=true` removes usable inventory slots while retaining equipment slots.

Upstream notes that equipped items persist per familiar, but servant inventory contents may not persist across restarts and are dropped when the servant dies or is destroyed. Do not advertise familiar inventory as durable storage.

## PvP and combat

`FamiliarPvP=false` causes familiars to unbind when entering PvP combat. Players cannot toggle familiar combat while in PvE/PvP combat or certain forms. Test castle, arena, and siege interactions under the server’s actual mod stack.

## Admin commands

- `.fam a [PlayerName] [PrefabGuid/CHAR_Unit_Name]` — testing/add operation.
- `.fam sl [Player] [Level]` — set active familiar level.
- `.fam sba` — set battle arena center.
- `.fam reset` — player recovery command that clears stale followers/active state; use normal unbind when possible.

Back up all directories under `BepInEx/config/Bloodcraft/Familiars/` before migrations.
---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
