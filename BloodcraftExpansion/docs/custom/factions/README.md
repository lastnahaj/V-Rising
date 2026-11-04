# Factions

!!! expansion "Custom / Planned Content"
    This category is a design placeholder for BloodcraftExpansion and is not an upstream Bloodcraft feature or an implemented system.

Factions are long-form allegiance and reputation tracks that shape access, objectives, and narrative identity. They must not silently reuse or overwrite Bloodcraft progression records.

## Design requirements

- faction roster, relationships, reputation range, tiers, decay, and lockouts;
- gain/loss sources with anti-farming and party-attribution rules;
- exclusivity, betrayal, reconciliation, and seasonal-reset policy;
- unlock ownership and behavior when reputation falls;
- interaction with clans, PvP, bloodlines, bounties, and world events;
- admin inspect, set, audit, and migration operations;
- player-facing explanation of irreversible or costly choices.

## First specification target

Define two opposed factions and one neutral path on paper, then prove a single reversible reputation transaction with no gameplay reward. Rewards should follow only after persistence and migration tests pass.

No faction hooks, NPCs, rewards, or PrefabGUIDs are selected by this page.

---

[Expansion Hub](../README.md) · [Design Standards](../DESIGN-STANDARDS.md) · [Roadmap](../ROADMAP.md)
