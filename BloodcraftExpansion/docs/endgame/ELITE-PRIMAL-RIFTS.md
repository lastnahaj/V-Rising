# Elite Primal Rifts

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

!!! experimental "Experimental Bloodcraft Feature"
    This opt-in system has an upstream experimental, WIP, or known-issue signal. Validate it in a test environment.

**Status:** WIP in the stable upstream README.

`ElitePrimalRifts=true` enables amplified Primal Rift/Primal War behavior. `RiftFrequency` controls how many rifts Bloodcraft starts per day, with source documentation stating a maximum of 24; `0` disables scheduled starts.

The feature is tied into Eclipse update scheduling when active. Test spawn cadence, restart behavior, cleanup, reward balance, and overlap with other event mods.

!!! deprecated "Stable-version warning"
    Bloodcraft 1.13.26 prerelease fixes duplicate scaling across restarts for persisted Nightmare and Primal Rift units. That fix is not part of the documented 1.13.22 stable package.
---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
