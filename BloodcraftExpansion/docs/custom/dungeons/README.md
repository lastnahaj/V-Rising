# Dungeons

!!! expansion "Custom / Planned Content"
    This category is a design placeholder for BloodcraftExpansion and is not an upstream Bloodcraft feature or an implemented system.

Dungeons are multi-stage encounter sequences with party ownership, checkpoints, failure recovery, and a complete cleanup lifecycle. “Instanced” behavior is not assumed; a safely bounded world-space model is acceptable.

## Design requirements

- entry eligibility, party lock, capacity, timeout, and re-entry policy;
- reserved-area or instance ownership with collision prevention;
- stage graph, checkpoint, wipe, abandonment, and completion states;
- unit, object, effect, and reward cleanup;
- death, disconnect, party change, server restart, and admin-cancel recovery;
- reward lockout and duplicate protection;
- performance budget and concurrency limits appropriate to the target server.

## First specification target

Create a paper specification for one bounded two-stage sequence, then validate reservation and cleanup without enemies. Combat content should follow only after world ownership and recovery are reliable.

No instance API, map location, hook, enemy, reward, or PrefabGUID is selected by this page.

---

[Expansion Hub](../README.md) · [Design Standards](../DESIGN-STANDARDS.md) · [Roadmap](../ROADMAP.md)
