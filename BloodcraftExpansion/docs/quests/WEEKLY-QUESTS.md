# Weekly Quests

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.26 stable<br>
    **Last verified:** 2026-11-02<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

Weekly quests use the same kill, gather, and crafting framework as daily quests but use weekly progress and reward cadence. Use `.quest p weekly` to inspect the current assignment and `.quest t weekly` to track an eligible target.

The stable upstream implementation applies a ×5 multiplier to configured reward quantities for weeklies. Use `.quest r weekly` to reroll an incomplete weekly for `RerollWeeklyPrefab` and `RerollWeeklyAmount`; a completed weekly cannot be rerolled.

---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
