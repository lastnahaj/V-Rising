# Leveling Guide

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

## Efficient progression without guessing formulas

Bloodcraft awards XP primarily from kills, but final values depend on server configuration and unit context. Rather than rely on a copied formula, use `.lvl get` and temporarily enable `.lvl log` to observe the live server.

### Player checklist

- Fight units near your level when `LevelScalingMultiplier` is nonzero.
- Stay within `ExpShareDistance`, in combat, and within the allowed level range when sharing.
- Rest in a coffin when rested XP is enabled; stone coffins grant the full configured accumulation and wooden coffins half.
- Check prestige requirements before reaching the cap so you understand the reset.

### Administrator checklist

- Set `StartingLevel` no higher than the experience you want new players to skip.
- Tune unit, V Blood, docile, war-event, group, spawner, and level-gap multipliers as one model.
- Decide whether PvE parties and PvP clans should share progress.
- Use `.lvl ignore [Player]` only for moderation/eligibility exceptions.

The default maximum is 90, but server values are authoritative.
---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
