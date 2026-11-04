# BloodcraftExpansion Roadmap

!!! expansion "Custom / Planned Content"
    This roadmap defines readiness gates. It does not claim an implemented BloodcraftExpansion gameplay feature or a release date.

## Delivery strategy

The project advances by evidence, not roster size. A phase completes only when its exit criteria are met for named dependency versions.

| Phase | Scope | Required outcome |
|---|---|---|
| 0. Architecture evidence | Ownership, hooks, identity, persistence, input, presentation, compatibility | Written decisions backed by targeted runtime tests |
| 1. Framework | Config validation, durable data, selection/respec, resource abstraction, diagnostics | Empty framework survives restart, rollback, disable, rename, and invalid configuration |
| 2. Technical slice | Dreadborn + Blood Reaver + Red Juggernaut | End-to-end loop proves server authority and lifecycle safety |
| 3. Balance pilot | Add Voidtouched + Spellblade and one specialization each | Four pairings establish tuning budgets and expose dominant-combination risks |
| 4. Full identity roster | Eight bloodlines, ten expansion classes, three specializations each | Every entry meets the content specification and interaction matrix |
| 5. World integration | Encounters, bosses, affixes, relics, factions, events, bounties, dungeons | Each category has an independent loop, recovery plan, and tested reward ownership |
| 6. Release candidate | Administration, migration, compatibility, documentation, packaging | Clean install/upgrade/downgrade tests and published support matrix pass |

## Phase 0 — Architecture evidence

- Confirm exact supported versions of V Rising, Bloodcraft, BepInExPack, VampireCommandFramework, and optional Eclipse.
- Validate every required game event and patch point in a dedicated-server test environment.
- Decide stable player identity and versioned storage format.
- Select an action channel that does not collide with Bloodcraft Shift state or common ability mods.
- Define chat-first feedback and optional client presentation boundaries.
- Specify dependency mismatch, downgrade, rollback, and uninstall behavior.

**Gate:** all decisions listed in [Architecture Boundaries](ARCHITECTURE.md) are recorded; no unresolved question is silently converted into an assumption.

## Phase 1 — Framework

- Global and per-entry enable switches.
- Strict schema/configuration validation with actionable errors.
- Separate expansion persistence with atomic or recoverable writes.
- Bloodline, expansion-class, and specialization selection records.
- Respec transactions that remove prior effects before applying new state.
- Resource-state abstraction with generation/spend frequency caps.
- Admin inspect, set, reset, migrate, and diagnostics operations.
- Optional Bloodcraft adapter that can fail closed without affecting unrelated state.

**Gate:** automated and runtime tests cover first selection, respec, death, disconnect, restart, disabled definitions, renamed IDs, corrupted records, dependency mismatch, and rollback.

## Phase 2 — Technical vertical slice

The first slice deliberately contains one complete path:

- **Bloodline:** Dreadborn
- **Expansion class:** Blood Reaver
- **Specialization:** Red Juggernaut

This path exercises passive modifiers, a combat resource, risk/reward state, specialization, administration, persistence, and recovery using observable combat events. It avoids making stealth, teleport replay, familiar control, or mandatory client UI a prerequisite for architecture validation.

**Gate:** the slice meets [Design Standards](DESIGN-STANDARDS.md), remains understandable through chat fallback, and survives a controlled multi-restart playtest without duplicate effects or lost durable state.

## Phase 3 — Balance pilot

Add Voidtouched and Spellblade with one specialization each. A 2×2 roster creates four pairings and contrasting durability/spell/hybrid patterns without committing to the full catalog.

Record:

- progression time and respec behavior;
- resource generation, spend rate, uptime, and waste;
- damage, sustain, mobility, and control outliers;
- boss normalization and multi-hit abuse;
- PvP burst/control outcomes;
- pairing pick rates and performance deltas.

**Gate:** no pairing is mandatory, numeric budgets are documented, and unresolved outliers have explicit decisions.

## Phase 4 — Full identity roster

Expand only through the same content template. Each entry requires stable IDs, exact mechanics, lifecycle rules, configuration, feedback, interaction coverage, tests, and an administration story.

**Gate:** all 8 bloodlines, 10 expansion classes, 30 class specializations, and bloodline evolutions meet Specification status before the roster enters full Playtest status.

## Phase 5 — World integration

Future categories must remain independently disabled and must not become hidden prerequisites for the identity framework. Reward sources must name the owning system, duplication protection, reset cadence, failure recovery, and removal behavior.

## Release readiness

A release candidate requires:

- named dependency versions and compatibility matrix;
- clean install, upgrade, downgrade, disable, uninstall, backup, and restore procedures;
- validated configuration with safe defaults;
- player/admin command and feedback documentation matching runtime behavior;
- migration tests for every released schema version;
- performance and soak-test results appropriate to the target population;
- known limitations and support boundaries;
- no writes to upstream Bloodcraft persistence.

---

[Expansion Hub](README.md) · [Architecture](ARCHITECTURE.md) · [Design Standards](DESIGN-STANDARDS.md)
