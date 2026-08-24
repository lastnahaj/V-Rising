# Player Commands

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

These 72 commands are declared with `adminOnly: false` in stable source. A system may still need to be enabled in server configuration.

## Blood

| Command | Shortcut | Arguments | Description | Admin |
|---|---|---|---|:---:|
| `.blood choosestat` | `.bl cst` | `[BloodOrStat] [BloodStat]` | Choose a bonus stat to enhance for your blood legacy. | No |
| `.blood get` | `.bl get` | `[BloodType]` | Display current blood legacy details. | No |
| `.blood list` | `.bl l` | — | Lists blood legacies available. | No |
| `.blood liststats` | `.bl lst` | — | Lists blood stats available. | No |
| `.blood log` | `.bl log` | — | Toggles Legacy progress logging. | No |
| `.blood resetstats` | `.bl rst` | — | Reset stats for current blood. | No |

## Class

| Command | Shortcut | Arguments | Description | Admin |
|---|---|---|---|:---:|
| `.class change` | `.class c` | `[Class]` | Change classes. | No |
| `.class choosespell` | `.class csp` | `[#]` | Sets shift spell for class if prestige level is high enough. | No |
| `.class list` | `.class l` | — | List available classes. | No |
| `.class listspells` | `.class lsp` | `[Class]` | Shows spells that can be gained from class. | No |
| `.class liststats` | `.class lst` | `[Class]` | List weapon and blood stat synergies for a class. | No |
| `.class lockshift` | `.class shift` | — | Toggle shift spell. | No |
| `.class select` | `.class s` | `[Class]` | Select class. | No |

## Familiar

| Command | Shortcut | Arguments | Description | Admin |
|---|---|---|---|:---:|
| `.familiar addbattlegroup` | `.fam abg` | `[BattleGroup]` | Creates new battle group. | No |
| `.familiar addbox` | `.fam ab` | `[BoxName]` | Adds empty box with name. | No |
| `.familiar bind` | `.fam b` | `[#]` | Activates specified familiar from current list. | No |
| `.familiar challenge` | `.fam challenge` | `[PlayerName]` | Challenges a player to battle or displays queue details. | No |
| `.familiar choosebattlegroup` | `.fam cbg` | `[BattleGroup]` | Sets active battle group. | No |
| `.familiar choosebox` | `.fam cb` | `[Name]` | Choose active box of familiars. | No |
| `.familiar deletebattlegroup` | `.fam dbg` | `[BattleGroup]` | Deletes a battle group. | No |
| `.familiar deletebox` | `.fam db` | `[BoxName]` | Deletes specified box if empty. | No |
| `.familiar echoes` | `.fam echoes` | `[VBloodName]` | VBlood purchasing for exo reward with quantity scaling to unit tier. | No |
| `.familiar emoteactions` | `.fam actions` | — | Shows available emote actions. | No |
| `.familiar emotes` | `.fam e` | — | Toggle emote actions. | No |
| `.familiar getlevel` | `.fam gl` | — | Display current familiar leveling progress. | No |
| `.familiar list` | `.fam l` | — | Lists unlocked familiars from current box. | No |
| `.familiar listbattlegroup` | `.fam bg` | `[BattleGroup]` | Displays details of the specified battle group, or the active one if none is given. | No |
| `.familiar listbattlegroups` | `.fam bgs` | — | Lists available battle groups. | No |
| `.familiar listboxes` | `.fam boxes` | — | Shows the available familiar boxes. | No |
| `.familiar movebox` | `.fam mb` | `[BoxName]` | Moves active familiar to specified box. | No |
| `.familiar prestige` | `.fam pr` | — | Prestiges familiar if conditions are met, raising base stats by configured multiplier. | No |
| `.familiar remove` | `.fam r` | `[#]` | Removes familiar from current set permanently. | No |
| `.familiar renamebox` | `.fam rb` | `[CurrentName] [NewName]` | Renames a box. | No |
| `.familiar reset` | `.fam reset` | — | Resets (destroys) entities found in followerbuffer and clears familiar actives data. | No |
| `.familiar search` | `.fam s` | `[Name]` | Searches boxes for familiar(s) with matching name. | No |
| `.familiar shinybuff` | `.fam shiny` | `[SpellSchool]` | Spend vampiric dust to make your familiar shiny! | No |
| `.familiar slotbattlegroup` | `.fam sbg` | `[BattleGroupOrSlot] [Slot]` | Assigns active familiar to a battle group slot. If no battle group is specified, assigns to active group. | No |
| `.familiar smartbind` | `.fam sb` | `[Name]` | Searches and binds a familiar. If multiple matches are found, returns a list for clarification. | No |
| `.familiar toggle` | `.fam t` | — | Calls or dismisses familar. | No |
| `.familiar togglecombat` | `.fam c` | — | Enables or disables combat for familiar. | No |
| `.familiar toggleoption` | `.fam option` | `[Setting]` | Toggles various familiar settings. | No |
| `.familiar unbind` | `.fam ub` | — | Destroys active familiar. | No |

## Level

| Command | Shortcut | Arguments | Description | Admin |
|---|---|---|---|:---:|
| `.level get` | `.lvl get` | — | Display current leveling progress. | No |
| `.level log` | `.lvl log` | — | Toggles leveling progress logging. | No |

## Miscellaneous

| Command | Shortcut | Arguments | Description | Admin |
|---|---|---|---|:---:|
| `.miscellaneous prepareforthehunt` | `.misc prepare` | — | Completes GettingReadyForTheHunt if not already completed. | No |
| `.miscellaneous reminders` | `.misc remindme` | — | Toggles general reminders for various mod features. | No |
| `.miscellaneous sct` | `.misc sct` | `[Type]` | Toggles various scrolling text elements. | No |
| `.miscellaneous silence` | `.misc silence` | — | Resets stuck combat music if needed. | No |
| `.miscellaneous starterkit` | `.misc kitme` | — | Provides starting kit. | No |
| `.miscellaneous userstats` | `.misc userstats` | — | Shows neat information about the player. | No |

## Prestige

| Command | Shortcut | Arguments | Description | Admin |
|---|---|---|---|:---:|
| `.prestige exoform` | — | — | Toggles taunting to enter exoform. | No |
| `.prestige get` | — | `[PrestigeType]` | Shows information about player's prestige status. | No |
| `.prestige leaderboard` | `.prestige lb` | `[PrestigeType]` | Lists prestige leaderboard for type. | No |
| `.prestige list` | `.prestige l` | — | Lists prestiges available. | No |
| `.prestige permashroud` | `.prestige shroud` | — | Toggles permashroud if applicable. | No |
| `.prestige selectform` | `.prestige sf` | `[EvolvedVampire\|CorruptedSerpent]` | Select active exoform shapeshift. | No |
| `.prestige self` | `.prestige me` | `[PrestigeType]` | Handles player prestiging. | No |
| `.prestige syncbuffs` | `.prestige sb` | — | Applies prestige buffs appropriately if not present. | No |

## Profession

| Command | Shortcut | Arguments | Description | Admin |
|---|---|---|---|:---:|
| `.profession get` | `.prof get` | `[Profession]` | Display your current profession progress. | No |
| `.profession list` | `.prof l` | — | Lists professions available. | No |
| `.profession log` | `.prof log` | — | Toggles profession progress logging. | No |

## Quest

| Command | Shortcut | Arguments | Description | Admin |
|---|---|---|---|:---:|
| `.quest log` | — | — | Toggles quest progress logging. | No |
| `.quest progress` | `.quest p` | `[QuestType]` | Display your current quest progress. | No |
| `.quest reroll` | `.quest r` | `[QuestType]` | Reroll quest for cost (daily only currently). | No |
| `.quest track` | `.quest t` | `[QuestType]` | Locate and track quest target. | No |

## Weapon

| Command | Shortcut | Arguments | Description | Admin |
|---|---|---|---|:---:|
| `.weapon choosestat` | `.wep cst` | `[WeaponOrStat] [WeaponStat]` | Choose a weapon stat to enhance based on your expertise. | No |
| `.weapon get` | `.wep get` | — | Displays current weapon expertise details. | No |
| `.weapon list` | `.wep l` | — | Lists weapon expertises available. | No |
| `.weapon liststats` | `.wep lst` | — | Lists weapon stats available. | No |
| `.weapon lockspells` | `.wep locksp` | — | Locks in the next spells equipped to use in your unarmed slots. | No |
| `.weapon log` | `.wep log` | — | Toggles expertise logging. | No |
| `.weapon resetstats` | `.wep rst` | — | Reset the stats for current weapon. | No |

---

[Wiki Home](../HOME.md) · [Commands](COMMANDS.md) · [Configuration](CONFIGURATION.md)
