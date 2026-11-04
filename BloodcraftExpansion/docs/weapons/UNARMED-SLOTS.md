# Unarmed and Fishing Slots

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.26 stable<br>
    **Last verified:** 2026-11-02<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

Bloodcraft includes Unarmed and FishingPole in its expertise type list. `UnarmedSlots` and `FishingSlots` are boolean switches that enable the extra-spell capture behavior for those loadouts. They are not PrefabGUID lists.

Players use `.wep locksp` to lock the next equipped spells into the enabled unarmed loadout. `Duality=true` allows both unarmed slots; `false` limits unarmed capture to one slot and does not affect fishing. Administrators can use `.wep spell [Name] [Slot] [PrefabGuid] [Radius]` for controlled testing, but any supplied PrefabGUID must be verified for the active game build.

---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
