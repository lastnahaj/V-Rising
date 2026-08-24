# Professions Configuration

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

## Professions

| Setting | Type | Default | Description |
|---|---|---|---|
| `ProfessionSystem` | bool | `False` | Enable or disable the profession system. |
| `ProfessionFactor` | float | `1` | The multiplier for profession experience. |
| `DisabledProfessions` | string | `empty string` | Professions that should be inactive separated by comma. |
| `ExtraRecipes` | bool (PrefabGUID list) | `False` | Enable or disable extra recipes. Players will not be able to add/change shiny buffs for familiars without this unless other means of obtaining vampiric dust are provided, salvage additions are controlled by this setting as well. See 'Recipes' section in README for complete list of changes. |

Exact defaults are shown for stable 1.13.22. Only constraints explicitly stated in the descriptions should be treated as supported ranges.

---

[Wiki Home](../HOME.md) · [Commands](COMMANDS.md) · [Configuration](CONFIGURATION.md)
