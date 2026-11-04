# Long-Progression Profile

!!! admin "Community example — not an upstream default"
    This overlay slows several progression tracks. Apply it before a season begins or with an announced migration plan; changing rates mid-season affects player equity.

```ini
[Leveling]
UnitLevelingMultiplier = 5
VBloodLevelingMultiplier = 10
GroupLevelingMultiplier = 1
UnitSpawnerMultiplier = 0
LevelScalingMultiplier = 0.05

[Expertise]
UnitExpertiseMultiplier = 1.5
VBloodExpertiseMultiplier = 4
UnitSpawnerExpertiseFactor = 0
MaxExpertisePrestiges = 10

[Legacies]
UnitLegacyMultiplier = 0.75
VBloodLegacyMultiplier = 4
MaxLegacyPrestiges = 10

[Professions]
ProfessionFactor = 0.75

[Prestige]
PrestigeSystem = true
MaxLevelingPrestiges = 10
LevelingPrestigeReducer = 0.05
PrestigeRatesReducer = 0.1
PrestigeStatMultiplier = 0.1
PrestigeRateMultiplier = 0.1
ExoPrestiging = false
Leaderboard = true
```

## Operational requirements

- Publish reset effects, caps, leaderboards, and rate changes before players invest.
- Measure median time per level, expertise, legacy, profession, and prestige track during a pilot.
- Keep Exo Prestige disabled until ordinary prestige completion and recovery have been observed.
- Review expertise/legacy stat growth together with class synergy; long progression must not become an uncapped combat-power gap.
- Store versioned configuration and data backups outside the live server directory.

## Promotion gate

Do not promote this profile from pilot to production until the expected season can reach its intended milestones, catch-up policy is defined, and restore testing confirms that all progression tracks remain consistent as one backup set.

---

[Server Administration](README.md) · [Prestige](../systems/PRESTIGE.md) · [Configuration](../reference/CONFIGURATION.md)
