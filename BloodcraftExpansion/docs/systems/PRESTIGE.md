# Prestige System

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

Prestige resets a completed progression track and records a permanent prestige level. Bloodcraft supports leveling prestige, per-weapon expertise prestige, per-blood legacy prestige, and optional Exo prestige. Familiar prestige is a separate familiar feature.

## Effects

- Leveling prestige resets character level progress, reduces later leveling XP by `LevelingPrestigeReducer`, and increases expertise/legacy gain through `PrestigeRateMultiplier`.
- Expertise and legacy prestige reset the selected track, reduce later gain through `PrestigeRatesReducer`, and increase bonus-stat caps through `PrestigeStatMultiplier`.
- Leveling prestige unlocks class spells at `PrestigeLevelsToUnlockClassSpells`.
- Leaderboards can be enabled or disabled.

## Commands

- `.prestige l` — list prestige tracks.
- `.prestige get [PrestigeType]` — inspect progress.
- `.prestige me [PrestigeType]` — prestige an eligible track.
- `.prestige lb [PrestigeType]` — show a leaderboard.
- `.prestige sb` — resync applicable prestige buffs.

Exo prestige requires normal progression maxima and has separate rewards and forms. Read [Prestige Types](../progression/PRESTIGE-TYPES.md) and [Exo Prestige](../progression/EXO-PRESTIGE.md) before enabling it.
---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
