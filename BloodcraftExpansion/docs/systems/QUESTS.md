# Quests System

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

Bloodcraft provides daily and weekly kill, gather, and crafting objectives when `QuestSystem` is enabled.

## Player flow

- `.quest p [QuestType]` shows current daily or weekly progress.
- `.quest t [QuestType]` locates and tracks a target.
- `.quest log` toggles progress messages.
- `.quest r [QuestType]` rerolls; stable source currently permits daily rerolling through this command.

Reward items and quantities come from paired `QuestRewards` and `QuestRewardAmounts` lists. Weekly rewards multiply configured amounts by five, and V Blood kill quests multiply them by three. `DailyPerfectChance` separately controls the chance for a perfect gem from a daily.

Administrators can refresh both quest slots with `.quest rf [Name]` and force completion with `.quest c [Name] [QuestType]`.

See the [quest hub](../quests/README.md) and [quest configuration](../reference/CONFIG-QUESTS.md).
---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
