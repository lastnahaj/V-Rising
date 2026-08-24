# Quest Rerolling

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

Players reroll with `.quest reroll [daily/weekly]`. Daily and weekly rerolls each use a configurable item PrefabGUID and quantity:

- `RerollDailyPrefab` and `RerollDailyAmount`
- `RerollWeeklyPrefab` and `RerollWeeklyAmount`

A weekly quest cannot be rerolled after it is completed. Administrators also have source-defined completion and refresh commands in the [admin command reference](../reference/ADMIN-COMMANDS.md).

---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
