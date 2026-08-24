# Configuration Reference

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

Bloodcraft generates `BepInEx/config/io.zfolmt.Bloodcraft.cfg`. Stop the server before editing and restart it after changes. The 153 entries below are sourced from stable 1.13.22; key capitalization and defaults are preserved.

| Group | Keys | Reference |
|---|---:|---|
| General + StarterKit | 17 | [General](CONFIG-GENERAL.md) |
| Leveling | 17 | [Leveling](CONFIG-LEVELING.md) |
| Expertise | 25 | [Expertise](CONFIG-EXPERTISE.md) |
| Legacies | 20 | [Legacies](CONFIG-LEGACIES.md) |
| Classes | 25 | [Classes](CONFIG-CLASSES.md) |
| Familiars | 23 | [Familiars](CONFIG-FAMILIARS.md) |
| Prestige | 13 | [Prestige](CONFIG-PRESTIGE.md) |
| Professions | 4 | [Professions](CONFIG-PROFESSIONS.md) |
| Quests | 9 | [Quests](CONFIG-QUESTS.md) |
| **Total** | **153** | |

!!! warning "PrefabGUID values"
    Numeric IDs and comma-separated ID lists are game PrefabGUIDs. Preserve verified values; this wiki does not infer friendly meanings for undocumented IDs.

## Source discrepancy

The stable source contains `UseEmberglassEclipseBridge`, while the generated stable README configuration list contains 152 entries and omits that key. This reference follows source code and therefore documents 153.

---

[Wiki Home](../HOME.md) · [Commands](COMMANDS.md) · [Configuration](CONFIGURATION.md)
