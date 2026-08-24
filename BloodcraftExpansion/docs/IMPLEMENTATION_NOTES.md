# Implementation Notes

!!! expansion "BloodcraftExpansion Content"
    These notes define constraints for future custom implementation. No gameplay code is included in this documentation project.

The stable Bloodcraft 1.13.22 source reviewed for this wiki does not document a public expansion API for third-party Bloodlines, Classes, or specializations. Future implementation should therefore begin with an explicit integration design rather than assuming hooks, services, data formats, or Eclipse messages.

Key boundaries:

- keep BloodcraftExpansion persistence separate from `BepInEx/config/Bloodcraft/`;
- treat Bloodcraft class selection, Shift-slot state, statistics, and prestige records as upstream-owned;
- verify every game hook and PrefabGUID against the targeted V Rising build;
- make migrations and feature removal reversible;
- degrade safely when Bloodcraft or Eclipse versions do not match a tested integration;
- never present a custom client panel or chat message as upstream Bloodcraft output.

Implementation planning continues in the [BloodcraftExpansion Roadmap](custom/ROADMAP.md).

---

[Wiki Home](HOME.md) · [Commands](reference/COMMANDS.md) · [Configuration](reference/CONFIGURATION.md)
