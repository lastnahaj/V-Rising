# Admin Command Operations

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

Use the complete source-derived [Admin Commands](../reference/ADMIN-COMMANDS.md) table for syntax. Admin operations include setting player progression, controlling quest state, testing spells, and managing familiar battle state.

Before a bulk or destructive progression operation:

1. stop normal player activity;
2. back up `BepInEx/config/Bloodcraft/`;
3. record the player, system, old value, and intended new value;
4. execute the smallest scoped command;
5. verify the result with the corresponding player-facing `get` command;
6. review logs before continuing.

Admin status is enforced by VampireCommandFramework. Do not expose administrative access merely to let players bypass progression.

---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
