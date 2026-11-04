# Relics

!!! expansion "Custom / Planned Content"
    This category is a design placeholder for BloodcraftExpansion and is not an upstream Bloodcraft feature or an implemented system.

Relics are build-defining modifiers for expansion mechanics. They should alter decisions or resource behavior instead of acting as another unconditional stat tier.

## Design requirements

- acquisition source, rarity, binding, ownership, and duplicate rules;
- equip/activate model that does not assume an unsafe inventory or equipment slot;
- explicit interaction with bloodline, expansion class, specialization, death, and respec;
- stacking category, cap, conflict tags, and removal behavior;
- safe response when a relic definition is disabled or renamed;
- salvage/refund policy and inventory-full handling;
- server configuration and admin recovery.

## First specification target

The first relic should modify one technical-slice resource transition without increasing its maximum damage ceiling. This proves data ownership and reversible application before a broader item economy is designed.

No item prefab, inventory hook, recipe, or drop source is selected by this page.

---

[Expansion Hub](../README.md) · [Design Standards](../DESIGN-STANDARDS.md) · [Roadmap](../ROADMAP.md)
