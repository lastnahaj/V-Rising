# Encounters

!!! expansion "Custom / Planned Content"
    This category is a design placeholder for BloodcraftExpansion and is not an upstream Bloodcraft feature or an implemented system.

Encounters are bounded, repeatable combat scenarios built from objectives, escalation rules, and explicit completion/failure states. They are smaller than dungeons and need not introduce a unique boss.

## Design requirements

- named start, active, success, failure, abandonment, and cleanup states;
- participation and reward eligibility that resist late-join and multi-account abuse;
- reset cadence, concurrency limit, timeout, and restart recovery;
- enemy ownership and cleanup that cannot leak units into the world;
- solo/party scaling policy and boss-resource normalization;
- reward ownership that does not duplicate upstream V Rising or Bloodcraft grants;
- admin inspect, cancel, and recover operations.

## First specification target

The first encounter should prove one location, one objective chain, one escalation rule, and one reward transaction without requiring a new map instance. It must recover cleanly after participant death, disconnect, server restart, and manual cancellation.

No hooks, PrefabGUIDs, reward tables, or compatibility claims are selected until the [architecture gates](../ARCHITECTURE.md) pass.

---

[Expansion Hub](../README.md) · [Design Standards](../DESIGN-STANDARDS.md) · [Roadmap](../ROADMAP.md)
