# Quest Rerolling

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.26 stable<br>
    **Last verified:** 2026-11-02<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

Players reroll with `.quest reroll [daily/weekly]`. Daily and weekly rerolls each use a configurable item PrefabGUID and quantity:

- `RerollDailyPrefab` and `RerollDailyAmount`
- `RerollWeeklyPrefab` and `RerollWeeklyAmount`

A weekly quest cannot be rerolled after it is completed. Administrators also have source-defined completion and refresh commands in the [admin command reference](../reference/ADMIN-COMMANDS.md).

!!! info "Upstream text discrepancy"
    The 1.13.26 command attribute still describes rerolling as daily-only, but the tagged implementation contains distinct Daily and Weekly branches and consumes the matching configured cost. This page follows implemented behavior.

---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
