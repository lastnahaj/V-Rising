# Nightmare Mode

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

!!! experimental "Experimental Bloodcraft Feature"
    This opt-in system has an upstream experimental, WIP, or known-issue signal. Validate it in a test environment.

**Status:** Optional in 1.13.22, with a known scaling fix in a newer prerelease.

`NightmareMode=true` applies PvE-only health, power, and movement tuning to non-player combat units. Stable changelog notes explicitly exclude attack-speed scaling.

## Known stable-version concern

Bloodcraft 1.13.26 prerelease fixes persisted units receiving duplicate Nightmare/Primal scaling across server restarts. Servers staying on 1.13.22 should treat repeated-restart behavior as a known risk and test after every restart.

Do not copy prerelease binaries into a stable deployment without intentionally choosing that prerelease. Track the [source/version notice](../SOURCES.md).
---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
