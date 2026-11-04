# World Events

!!! expansion "Custom / Planned Content"
    This category is a design placeholder for BloodcraftExpansion and is not an upstream Bloodcraft feature or an implemented system.

World events are scheduled, server-visible objectives that coordinate players around a shared state. They must coexist with V Rising events and Bloodcraft's optional Primal Rift behavior.

## Design requirements

- schedule, announcement, activation, participation, success, failure, and cleanup states;
- time-zone-neutral administration and clear player countdowns;
- concurrency and location-conflict rules;
- contribution scoring that handles parties, support actions, late joins, and disconnects;
- reward eligibility, lockouts, duplicate protection, and inventory-full recovery;
- restart resume/cancel policy and admin override;
- explicit compatibility plan for Primal Rifts and other event mods.

## First specification target

Prototype a non-rewarding scheduled announcement and state transition before spawning units or granting items. The full event must remain optional and recover without manual file edits.

No schedule, event hook, reward, unit, or PrefabGUID is selected by this page.

---

[Expansion Hub](../README.md) · [Design Standards](../DESIGN-STANDARDS.md) · [Roadmap](../ROADMAP.md)
