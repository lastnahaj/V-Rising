# Nightmare Mode

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.26 stable<br>
    **Last verified:** 2026-11-02<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

!!! experimental "Experimental Bloodcraft Feature"
    This opt-in system has an upstream experimental, WIP, or known-issue signal. Validate it in a test environment.

**Status:** Optional. The current 1.13.26 package includes the persisted-unit restart-scaling fix.

`NightmareMode=true` applies PvE-only health, power, and movement tuning to non-player combat units. Stable changelog notes explicitly exclude attack-speed scaling.

## Restart behavior

Bloodcraft 1.13.26 guards persisted Nightmare and Primal Rift units against receiving duplicate stat scaling across server restarts. This removes the specific 1.13.22 defect; it does not remove the need to verify affected units after upgrades, downgrades, or mod-stack changes.

Keep the package, configuration, and player/server backups versioned together. Track the [source/version notice](../SOURCES.md).
---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
