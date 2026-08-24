# Player Data

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

Bloodcraft persists JSON data below `BepInEx/config/Bloodcraft/`. Stable source creates system directories including:

- `PlayerLeveling/`
- `Quests/`
- `WeaponExpertise/`
- `BloodLegacies/`
- `Professions/`
- `Familiars/` with leveling, unlock, equipment, and battle-group subdirectories
- `PlayerBools/`

The root also stores class and party data. Files are keyed around player Steam IDs and system-specific records.

## Backup and restore

Stop the server before copying or restoring data so an in-memory save cannot overwrite the backup. Back up the whole Bloodcraft directory as one consistent set; progression records interact across systems. Preserve the config alongside the data because caps and rates affect how records are interpreted.

Do not hand-edit player JSON unless an upstream recovery procedure requires it. Use admin commands for supported changes and test restores on a copy.

!!! warning "Familiar inventory limitation"
    Stable upstream notes that familiar equipment persists, but ordinary contents placed in a familiar inventory may not survive restarts and can drop when the servant dies or is destroyed.

---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
