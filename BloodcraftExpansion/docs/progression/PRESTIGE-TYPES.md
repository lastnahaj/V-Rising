# Prestige Types

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

Bloodcraft defines 28 prestige identifiers.

## Character

| Type | Track | Default maximum |
|---|---|---:|
| `Experience` | Character leveling | 10 |
| `Exo` | Post-normal-prestige endgame | 100 in stable source description |

## Weapon Expertise Prestige

`SwordExpertise`, `AxeExpertise`, `MaceExpertise`, `SpearExpertise`, `CrossbowExpertise`, `GreatSwordExpertise`, `SlashersExpertise`, `PistolsExpertise`, `ReaperExpertise`, `LongbowExpertise`, `WhipExpertise`, `UnarmedExpertise`, `FishingPoleExpertise`, `TwinBladesExpertise`, `DaggersExpertise`, and `ClawsExpertise` use `MaxExpertisePrestiges` (default `10`).

## Blood Legacy Prestige

`WorkerLegacy`, `WarriorLegacy`, `ScholarLegacy`, `RogueLegacy`, `MutantLegacy`, `DraculinLegacy`, `ImmortalLegacy`, `CreatureLegacy`, `BruteLegacy`, and `CorruptionLegacy` use `MaxLegacyPrestiges` (default `10`).

## Shared commands

- `.prestige l` — list valid types.
- `.prestige get [PrestigeType]` — show eligibility and progress.
- `.prestige me [PrestigeType]` — reset an eligible track.
- `.prestige lb [PrestigeType]` — leaderboard.

Prestiging resets the selected progression data. Back up player data before changing caps or forcing admin resets.
---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
