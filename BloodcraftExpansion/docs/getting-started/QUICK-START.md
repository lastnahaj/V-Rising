# Quick Start

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

## Player path

1. Run `.lvl get` to check character level and experience.
2. Equip a weapon and run `.wep get`; use `.wep lst` before selecting expertise stats.
3. Feed and run `.bl get`; use `.bl lst` before selecting legacy stats.
4. Run `.class l`, inspect `.class lst [Class]`, then select with `.class s [Class]`.
5. Check daily or weekly progress with `.quest p [QuestType]`.
6. If familiars are enabled, review `.fam l`, `.fam b [#]`, and `.fam gl`.
7. Inspect professions with `.prof l` and `.prof get [Profession]`.
8. When a progression track reaches its configured maximum, inspect `.prestige get [PrestigeType]` before using `.prestige me [PrestigeType]`.

Systems can be disabled independently. A command may report that its system is unavailable; that is a server configuration choice, not necessarily an installation failure.

## Server administrator path

1. Install the stable server package and dependencies.
2. Launch once to generate configuration and data directories.
3. Stop the server and back up the generated files.
4. Enable only the systems you intend to support.
5. Restart so startup-cached settings are applied.
6. Run `.misc health` and inspect the server log.
7. Tune progression multipliers and caps deliberately; compare against [the exact defaults](../reference/CONFIGURATION.md).
8. Give players this page and the [player command reference](../reference/PLAYER-COMMANDS.md).

Start conservatively. Experimental systems such as Familiar Battles, Bleeding Edge, Primal Arsenal, and some endgame extras need explicit review before production use.
---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
