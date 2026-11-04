# Cooperative PvE Profile

!!! admin "Community example — not an upstream default"
    This overlay assumes the [recommended rollout profile](RECOMMENDED-CONFIG.md) has already been reviewed. Merge it into a generated 1.13.26 configuration while the server is stopped.

This profile favors shared progression and conservative familiar use. It does not enable experimental endgame or weapon systems.

```ini
[Leveling]
ExpShare = true
ExpShareLevelRange = 10
ExpShareDistance = 25
GroupLevelingMultiplier = 1
UnitSpawnerMultiplier = 0

[Classes]
ClassOnHitEffects = true
OnHitProcChance = 0.075
SynergyMultiplier = 1.5

[Familiars]
ShareUnlocks = true
FamiliarCombat = true
FamiliarPvP = false
AllowVBloods = false
AllowMinions = false
FamiliarBattles = false

[Professions]
ProfessionFactor = 1
ExtraRecipes = false

[Prestige]
PrestigeSystem = false
ExoPrestiging = false
```

## Deliberate choices

- Spawner XP remains disabled to avoid trivial automated progression.
- Familiar unlock sharing supports cooperative groups; V Bloods and minions remain excluded.
- Extra Recipes should be enabled only after the economy has a documented role for Vampiric Dust, Primal Jewels, and added conversions.
- Prestige should be introduced through an announced maintenance change after backup/restore testing.

## Validate before launch

Test party and clan sharing, familiar unlock attribution, boss kill credit, class on-hit effects, profession output, server restart, and inventory-full reward behavior with the expected group size.

---

[Server Administration](README.md) · [Recommended Rollout](RECOMMENDED-CONFIG.md) · [Configuration](../reference/CONFIGURATION.md)
