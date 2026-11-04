# Bounties

!!! expansion "Custom / Planned Content"
    This category is a design placeholder for BloodcraftExpansion and is not an upstream Bloodcraft feature or an implemented system.

Bounties are opt-in, repeatable hunt contracts with explicit targets, timers, eligibility, and rewards. They should complement rather than duplicate Bloodcraft daily/weekly quests.

## Design requirements

- assignment source, target pool, reroll/abandon rules, cadence, and limits;
- distinction from upstream Bloodcraft quest data and commands;
- kill attribution for solo, party, clan, familiar, damage-over-time, and environmental deaths;
- target availability, respawn, invalidation, and substitution behavior;
- reward transaction, duplicate protection, and inventory-full recovery;
- anti-farming, level-gap, and repeated-target controls;
- admin inspect, refresh, complete, cancel, and audit operations.

## First specification target

Define one ordinary-unit hunt using a separate expansion record and no item reward. Prove assignment, progress, completion, expiration, disconnect, and restart before adding V Blood targets or economies.

No quest hook, target table, reward, or PrefabGUID is selected by this page.

---

[Expansion Hub](../README.md) · [Design Standards](../DESIGN-STANDARDS.md) · [Roadmap](../ROADMAP.md)
