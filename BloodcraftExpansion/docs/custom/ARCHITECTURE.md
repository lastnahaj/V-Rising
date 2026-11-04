# Architecture Boundaries

!!! expansion "BloodcraftExpansion Design Decision"
    This page defines implementation boundaries for planned custom content. It does not claim that gameplay code, hooks, commands, or client UI currently exist.

## Product boundary

BloodcraftExpansion is designed as an optional server-content layer that **coexists with** Bloodcraft without taking ownership of Bloodcraft data.

| Layer | Owner | BloodcraftExpansion rule |
|---|---|---|
| Character level, expertise, blood legacies, upstream class, prestige, professions, quests, familiars | Bloodcraft | Read only through a version-tested adapter when integration is necessary; never rewrite or reinterpret upstream persistence. |
| Bloodline, expansion class, specialization, custom resources, custom unlocks | BloodcraftExpansion | Persist separately under a dedicated namespace and schema version. |
| Base combat, units, buffs, abilities, forms, world state | V Rising | Use only hooks and PrefabGUIDs verified for the targeted game build. |
| Optional presentation | Chat first; client integration later | Never impersonate Bloodcraft or Eclipse output. Client features must be optional unless a release explicitly changes that requirement. |

## Identity model

An **expansion class** is a separate identity layer; it does not replace or mutate the player's upstream Bloodcraft class selection. A player may therefore have:

1. one Bloodcraft class, owned by Bloodcraft;
2. one BloodcraftExpansion bloodline;
3. one BloodcraftExpansion class;
4. one specialization within that expansion class.

This dual-class model must be visible in terminology, commands, configuration, data, and UI. Public text should say **Bloodcraft class** or **expansion class** whenever ambiguity is possible.

## Integration model

The implementation should isolate optional upstream integration behind a small adapter boundary:

```text
Game events ──> validated event adapter ──> expansion domain rules
Bloodcraft ───> optional versioned adapter ┘          │
                                                     ├──> expansion persistence
                                                     ├──> admin/player feedback
                                                     └──> optional presentation bridge
```

The expansion domain must not depend directly on unverified Bloodcraft internals throughout the codebase. An unsupported Bloodcraft version should disable only the affected integration features, emit one actionable diagnostic, and preserve recoverable player data.

## Authority and state

- The server is authoritative for selection, progression, resource generation, spending, cooldowns, and effects.
- Clients may display state but must not decide outcomes.
- Short-lived combat resources normally reset on death, disconnect, or invalid form transitions unless a class specification explicitly defines safe persistence.
- Durable progression writes must be atomic or recoverable and include a schema version.
- Player identity must use the stable server-side identity selected during implementation research; display names are never persistence keys.

## Action-channel gate

Racial abilities and expansion-class actions require a verified input path. No design may assume ownership of Shift, weapon slots, emotes, forms, or Eclipse controls.

Before an active ability ships, the selected action channel must pass tests for:

- Bloodcraft Shift-slot coexistence;
- weapon swapping and unarmed/fishing slots;
- shapeshifts, mounts, castle interactions, and death;
- reconnect and server restart;
- common ability-mod conflicts;
- operation without a required client mod, unless the release explicitly declares one.

If no safe channel is validated, the first release should use passive or event-driven mechanics rather than claiming an active racial ability.

## Lifecycle contract

Every content definition must state what happens on:

| Event | Required decision |
|---|---|
| Selection or respec | Validate eligibility, remove previous effects, apply new state, persist once. |
| Death | Clear transient resources and temporary entities; retain durable progression. |
| Disconnect | Remove transient entities and combat state without losing durable choices. |
| Restart | Restore only durable state; never duplicate buffs, entities, or scheduled work. |
| Definition disabled | Suspend the entry safely and provide an admin-visible migration path. |
| Definition renamed | Migrate by stable ID, not display name. |
| Dependency mismatch | Disable affected integration, preserve data, and explain remediation. |
| Uninstall | Leave upstream Bloodcraft data untouched and document custom-data recovery/removal. |

## Required architecture decisions

Implementation cannot begin beyond prototypes until these decisions have written records:

1. supported V Rising, Bloodcraft, BepInEx, and VampireCommandFramework versions;
2. event and hook inventory with runtime evidence;
3. persistent player identity and storage format;
4. action/input channel;
5. configuration format and validation policy;
6. chat and optional client presentation contract;
7. migration, rollback, and feature-removal behavior;
8. compatibility test matrix.

See [Design Standards](DESIGN-STANDARDS.md) for content-level readiness and the [Roadmap](ROADMAP.md) for sequencing.

---

[Expansion Hub](README.md) · [Design Standards](DESIGN-STANDARDS.md) · [Roadmap](ROADMAP.md)
