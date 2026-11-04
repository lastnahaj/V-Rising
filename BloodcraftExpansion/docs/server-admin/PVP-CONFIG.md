# Conservative PvP Profile

!!! admin "Community example — not an upstream default"
    PvP balance is server-specific. This overlay reduces proc/synergy pressure and disables familiar PvP and Bloodcraft-managed Shift spells until controlled duel and group tests pass.

```ini
[Leveling]
ExpShare = true
ExpShareLevelRange = 10
ExpShareDistance = 25

[Expertise]
ExpertiseStatChoices = 3
ShiftSlot = false

[Legacies]
LegacyStatChoices = 3

[Classes]
ClassOnHitEffects = true
OnHitProcChance = 0.05
SynergyMultiplier = 1.25

[Familiars]
FamiliarPvP = false
AllowVBloods = false
AllowMinions = false
FamiliarBattles = false

[General]
PotionStacking = false
BleedingEdge =
PrimalArsenal = false
NightmareMode = false
EliteShardBearers = false
ElitePrimalRifts = false

[Prestige]
PrestigeSystem = false
ExoPrestiging = false
```

## Required balance pass

Before enabling Shift spells, familiar PvP, prestige stat scaling, Exo forms, potion stacking, or experimental weapons, test:

- equal-progression duels across all six upstream classes;
- outnumbered and group combat;
- physical/spell burst, sustain, movement, cooldown recovery, and control duration;
- gear and progression-gap outcomes;
- siege-window reconnects and class changes;
- familiar dismissal on PvP entry;
- seasonal reset and respec-cost communication.

Record the exact live configuration with every balance result. A configuration change without its tested context is not a reusable preset.

---

[Server Administration](README.md) · [Recommended Rollout](RECOMMENDED-CONFIG.md) · [Configuration](../reference/CONFIGURATION.md)
