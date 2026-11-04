# Admin Commands

!!! admin "Administrative access"
    These commands modify server or player state. Back up Bloodcraft player data before bulk changes.

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.26 stable<br>
    **Last verified:** 2026-11-02<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

These 16 commands are declared with `adminOnly: true` in stable source. Names, aliases, arguments, and flags follow source; descriptions are normalized for clarity.

## Blood

| Command | Shortcut | Arguments | Description | Admin |
|---|---|---|---|:---:|
| `.blood set` | `.bl set` | `[Player] [Blood] [Level]` | Sets player blood legacy level. | Yes |

## Familiar

| Command | Shortcut | Arguments | Description | Admin |
|---|---|---|---|:---:|
| `.familiar add` | `.fam a` | `[PlayerName] [PrefabGuid/CHAR_Unit_Name]` | Unit testing. | Yes |
| `.familiar setbattlearena` | `.fam sba` | — | Set current position as the center for the familiar battle arena. | Yes |
| `.familiar setlevel` | `.fam sl` | `[Player] [Level]` | Set current familiar level. | Yes |

## Level

| Command | Shortcut | Arguments | Description | Admin |
|---|---|---|---|:---:|
| `.level ignoresharedexperience` | `.lvl ignore` | `[Player]` | Adds (or removes) player to list of those who are not eligible to receive shared experience. | Yes |
| `.level set` | `.lvl set` | `[Player] [Level]` | Sets player level. | Yes |

## Miscellaneous

| Command | Shortcut | Arguments | Description | Admin |
|---|---|---|---|:---:|
| `.miscellaneous health` | `.misc health` | — | Shows startup readiness state summary. | Yes |

## Prestige

| Command | Shortcut | Arguments | Description | Admin |
|---|---|---|---|:---:|
| `.prestige iacknowledgethiswillremoveallprestigebuffsfromplayersandwantthattohappen` | — | — | Globally removes prestige buffs from players to facilitate changing prestige buffs in config. | Yes |
| `.prestige ignoreleaderboard` | `.prestige ignore` | `[Player]` | Adds (or removes) player to list of those who will not appear on prestige leaderboards. Intended for admin-duties only accounts. | Yes |
| `.prestige reset` | `.prestige r` | `[Player] [PrestigeType]` | Handles resetting prestiging. | Yes |
| `.prestige set` | — | `[Player] [PrestigeType] [Level]` | Sets the specified player to a certain level of prestige in a certain type of prestige. | Yes |

## Profession

| Command | Shortcut | Arguments | Description | Admin |
|---|---|---|---|:---:|
| `.profession set` | `.prof set` | `[Name] [Profession] [Level]` | Sets player profession level. | Yes |

## Quest

| Command | Shortcut | Arguments | Description | Admin |
|---|---|---|---|:---:|
| `.quest complete` | `.quest c` | `[Name] [QuestType]` | Forcibly completes a specified quest for a player. | Yes |
| `.quest refresh` | `.quest rf` | `[Name]` | Refreshes daily and weekly quests for player. | Yes |

## Weapon

| Command | Shortcut | Arguments | Description | Admin |
|---|---|---|---|:---:|
| `.weapon set` | `.wep set` | `[Name] [Weapon] [Level]` | Sets player weapon expertise level. | Yes |
| `.weapon setspells` | `.wep spell` | `[Name] [Slot] [PrefabGuid] [Radius]` | Set a spell for controlled testing; an optional radius applies it around the named player. | Yes |

---

[Wiki Home](../HOME.md) · [Commands](COMMANDS.md) · [Configuration](CONFIGURATION.md)
