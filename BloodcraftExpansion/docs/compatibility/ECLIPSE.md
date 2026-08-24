# Eclipse Compatibility

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable · **Eclipse version:** 1.3.14 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft and Eclipse, not BloodcraftExpansion custom features.

Eclipse is Bloodcraft's companion client UI. Bloodcraft runs on the server without requiring players to install Eclipse; clients add the visual interface.

Stable Eclipse displays progress or information for Leveling, Blood Legacies, Weapon Expertise, Familiars, Professions, Quests, and the Shift slot. It also adds Bloodcraft statistics to the character Attributes tab and can invoke stat-choice commands from those entries.

UI elements can be individually disabled. Bloodcraft's `Eclipsed` setting now controls server update frequency rather than whether the bridge is active: true is faster (0.1 seconds), false is slower (2.5 seconds). Eclipse has a corresponding frequency preference.

The source-only `UseEmberglassEclipseBridge` option selects Emberglass transport when available and otherwise falls back to the legacy chat bridge.

See [Eclipse Setup](../getting-started/ECLIPSE.md).

---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
