# Enemy Affixes

!!! expansion "Custom / Planned Content"
    This category is a design placeholder for BloodcraftExpansion and is not an upstream Bloodcraft feature or an implemented system.

Enemy affixes are composable modifiers that change how eligible enemies are read and fought. They should create counterplay, not merely inflate health or damage.

## Design requirements

- eligibility filters by unit category, level, location, encounter, and boss status;
- mutually exclusive tags and a maximum affix count;
- visible name, effect, telegraph, counter, and reward modifier;
- deterministic application and duplicate-effect protection across persistence/restarts;
- exclusions for unsafe minions, summons, servants, players, and scripted units;
- normalized interaction with class resources and kill attribution;
- cleanup on death, despawn, conversion, charm, and encounter cancellation.

## First specification target

Prototype one defensive and one positional affix with explicit counters. Avoid attack-speed, movement, or stacking multipliers until unit lifecycle and restart behavior are proven.

No affix values, buffs, hooks, or PrefabGUIDs are selected by this page.

---

[Expansion Hub](../README.md) · [Design Standards](../DESIGN-STANDARDS.md) · [Roadmap](../ROADMAP.md)
