# Recommended Rollout Profile

!!! admin "Community starting profile — not an upstream default"
    Generate the 1.13.26 configuration first, stop the server, back up the generated file and player data, then merge only the reviewed values. Do not replace a newer generated configuration wholesale.

This profile enables the foundational systems while leaving prestige, extra abilities, endgame modifiers, experimental weapons, and familiar PvP disabled for a staged launch.

```ini
[General]
ElitePrimalRifts = false
NightmareMode = false
EliteShardBearers = false
BleedingEdge =
PrimalArsenal = false

[Leveling]
LevelingSystem = true
RestedXPSystem = true
MaxLevel = 90
StartingLevel = 10
ExpShare = true
ExpShareLevelRange = 10
ExpShareDistance = 25

[Expertise]
ExpertiseSystem = true
MaxExpertiseLevel = 100
ExpertiseStatChoices = 3
UnarmedSlots = false
FishingSlots = false
ShiftSlot = false

[Legacies]
LegacySystem = true
MaxBloodLevel = 100
LegacyStatChoices = 3

[Classes]
ClassSystem = true
ClassOnHitEffects = true
OnHitProcChance = 0.075
SynergyMultiplier = 1.5

[Quests]
QuestSystem = true
InfiniteDailies = false

[Professions]
ProfessionSystem = true
ProfessionFactor = 1
ExtraRecipes = false

[Familiars]
FamiliarSystem = true
FamiliarCombat = true
FamiliarPvP = false
FamiliarBattles = false
FamiliarPrestige = false
AllowVBloods = false
AllowMinions = false
PrimalEchoes = false

[Prestige]
PrestigeSystem = false
ExoPrestiging = false
```

## Why this order

- Leveling, expertise, legacies, classes, quests, professions, and ordinary familiars can be observed independently.
- Shift spells remain off until their slot interactions are tested.
- Prestige remains off until players understand resets and the administrator has a backup/restore procedure.
- V Blood familiars, minions, familiar battles, Exo, Nightmare, elite systems, Bleeding Edge, and Primal Arsenal remain deliberate later decisions.

## Launch verification

1. Restart and confirm `Loaded [1.13.26]` in the server log.
2. Run `.misc health` as an administrator.
3. Confirm each enabled system with its player-facing list/get command.
4. Complete one ordinary progression action in each enabled system.
5. Stop the server, back up the data directory, restart, and verify persistence.
6. Publish enabled systems, reset costs, and support limitations to players.

Use the [exact configuration reference](../reference/CONFIGURATION.md) before changing omitted values.

---

[Server Administration](README.md) · [Configuration](../reference/CONFIGURATION.md) · [Troubleshooting](TROUBLESHOOTING.md)
