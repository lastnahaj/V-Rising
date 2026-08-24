# Mining

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

## Earning experience

Mining experience comes from breaking mineral-related resource objects and handling recognized mineral, stone, blood-crystal, tech-scrap, or emery items.

## Benefit

Bonus resources scale with profession level; upstream also documents gold ore as a profession-specific drop that can be salvaged into jewelry.

## Commands and configuration

- `.prof get Mining` — inspect progress.
- `.prof log` — toggle profession progress messages.
- `.prof set [Name] Mining [Level]` — administrator override.
- `ProfessionFactor` — global profession XP multiplier.
- `DisabledProfessions` — include `Mining` to disable this profession.

## Practical note

Mine varied node tiers and use `.prof get Mining` to verify progress before adjusting rates.

Return to the [Profession Overview](README.md) or open [Profession Configuration](../reference/CONFIG-PROFESSIONS.md).
---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
