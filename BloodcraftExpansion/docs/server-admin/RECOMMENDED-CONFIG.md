# Recommended Configuration Approach

!!! admin "Community configuration example — not an upstream Bloodcraft default"
    Bloodcraft ships major systems disabled. Introduce and tune them deliberately for your server population.

Begin with Leveling, Expertise, Legacies, Classes, Quests, Professions, Familiars, and Prestige as separate decisions. Enable dependent systems together: Prestige requires Leveling, class spell progression uses Prestige, and familiar Exo/Primal Echo play depends on its associated endgame options.

A safe rollout uses upstream defaults for rates and caps, then changes one group at a time. Keep experimental options—`BleedingEdge`, `PrimalArsenal`, `ElitePrimalRifts`, familiar battles, and other WIP features—off until tested.

Back up `io.zfolmt.Bloodcraft.cfg` and `BepInEx/config/Bloodcraft/` before changing progression rules. Announce resets, cap changes, and prestige changes to players in advance.

See the exact [configuration reference](../reference/CONFIGURATION.md).

---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
