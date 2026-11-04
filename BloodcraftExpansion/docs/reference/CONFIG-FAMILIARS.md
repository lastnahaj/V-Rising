# Familiars Configuration

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.26 stable<br>
    **Last verified:** 2026-11-02<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

## Familiars

| Setting | Type | Default | Description |
|---|---|---|---|
| `FamiliarSystem` | bool | `False` | Enable or disable the familiar system. |
| `ShareUnlocks` | bool | `False` | Enable or disable sharing unlocks between players in clans or parties (uses exp share distance). |
| `FamiliarCombat` | bool | `True` | Enable or disable combat for familiars. |
| `FamiliarPvP` | bool | `True` | Enable or disable PvP participation for familiars. (if set to false, familiars will be unbound when entering PvP combat). |
| `FamiliarBattles` | bool | `False` | Enable the familiar battle system. Upstream warns that it is most likely not working after V Rising 1.1; keep disabled unless a current staging test passes. |
| `FamiliarPrestige` | bool | `False` | Enable or disable the prestige system for familiars. |
| `MaxFamiliarPrestiges` | int | `10` | The maximum number of prestiges a familiar can reach. |
| `FamiliarPrestigeStatMultiplier` | float | `0.1` | The multiplier for applicable stats gained per familiar prestige. |
| `MaxFamiliarLevel` | int | `90` | The maximum level a familiar can reach. |
| `AllowVBloods` | bool | `False` | Allow V Bloods, including shard bearers, to be unlocked as familiars. Use `BannedUnits` to exclude specific units. |
| `AllowMinions` | bool | `False` | Allow minion units to be unlocked as familiars. Upstream excludes them by default because some have unsafe or undesirable behavior. |
| `BannedUnits` | string (PrefabGUID list) | `empty string` | The PrefabGUID hashes for units that cannot be used as familiars. Same structure as the buff lists except unit prefabs. |
| `BannedTypes` | string | `empty string` | The types of units that cannot be used as familiars go here (Human, Undead, Demon, Mechanical, Beast). |
| `EquipmentOnly` | bool | `False` | True for only equipment with no working inventory slots, false for both. |
| `UnitFamiliarMultiplier` | float | `7.5` | The multiplier for experience gained from units. |
| `VBloodFamiliarMultiplier` | float | `15` | The multiplier for experience gained from VBloods. |
| `UnitUnlockChance` | float | `0.05` | The chance for a unit unlock as a familiar. |
| `VBloodUnlockChance` | float | `0.01` | The chance for a VBlood unlock as a familiar. |
| `PrimalEchoes` | bool | `False` | Allow eligible V Blood familiar unlocks to be purchased with the configured Exo reward. Cost scales from `ExoPrestigeRewardQuantity` by unit tier; shard bearers use the highest documented factor of 25. Requires normal and Exo prestige and respects familiar bans. |
| `EchoesFactor` | int | `1` | Increase to multiply costs for vBlood purchases. Valid integers are between 1-4, if values are outside that range they will be clamped. |
| `ShinyChance` | float | `0.2` | The chance for a shiny when unlocking familiars (6 total buffs, 1 buff per familiar). Guaranteed on second unlock of same unit when chance is greater than zero; damage proc chance follows the configured on-hit effect chance. |
| `ShinyCostItemQuantity` | int | `100` | Quantity of vampiric dust required to make a familiar shiny. May also be spent to change shiny familiar's shiny buff at 25% cost. Enable ExtraRecipes to allow player refinement of this item from Advanced Grinders. Valid values are between 50-200, if outside that range in either direction it will be clamped. |
| `PrestigeCostItemQuantity` | int | `1000` | Quantity of schematics required to immediately prestige familiar (gain total levels equal to max familiar level, extra levels remaining from the amount needed to prestige will be added to familiar after prestiging). Valid values are between 500-2000, if outside that range in either direction it will be clamped. |

Exact defaults are shown for stable 1.13.26. Only constraints explicitly stated in the descriptions should be treated as supported ranges.

---

[Wiki Home](../HOME.md) · [Commands](COMMANDS.md) · [Configuration](CONFIGURATION.md)
