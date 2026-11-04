# Custom Bosses

!!! expansion "Custom / Planned Content"
    This category is a design placeholder for BloodcraftExpansion and is not an upstream Bloodcraft feature or an implemented system.

Custom bosses are authored high-signal encounters with recognizable phases, readable counters, explicit failure recovery, and rewards that serve the expansion without invalidating V Blood progression.

## Design requirements

- encounter purpose, target group size, expected duration, and gear/progression assumptions;
- phase transitions based on authoritative state rather than fragile timing alone;
- immunity, control, summon, leashing, reset, and arena-boundary rules;
- normalized interaction with class resources, execute mechanics, life leech, and familiars;
- reward lockout, eligibility, duplicate protection, and inventory-full handling;
- death, wipe, disconnect, restart, despawn, and admin-cancel recovery;
- named telemetry for phase time, deaths, damage spikes, and failure causes.

## First specification target

Design one three-phase boss using verified existing game components before considering custom client assets. The prototype must demonstrate clean spawn/despawn ownership and must not alter an upstream V Blood record.

No boss, reward, hook, or PrefabGUID is approved by this concept page.

---

[Expansion Hub](../README.md) · [Design Standards](../DESIGN-STANDARDS.md) · [Roadmap](../ROADMAP.md)
