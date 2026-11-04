# Design Standards

!!! expansion "BloodcraftExpansion Content Standard"
    These standards govern proposed expansion content. They are acceptance criteria for design work, not evidence of implementation.

## Content status

Every page must use one of these states:

| State | Meaning |
|---|---|
| Concept | Fantasy and intended loop exist; mechanics are incomplete. |
| Specification | Triggers, values, lifecycle, configuration, and tests are defined. |
| Prototype | Implemented only for isolated technical validation. |
| Playtest | Complete enough for controlled balance and compatibility testing. |
| Release candidate | Migration, rollback, administration, and documentation are verified. |
| Released | Shipped for named dependency versions with published limitations. |

The current Bloodlines & Classes roster is **Concept** status unless a page explicitly advances an entry.

## Required content specification

A bloodline, class, specialization, synergy, relic, affix, encounter, or boss is not implementation-ready until it defines:

1. player fantasy and intended decision;
2. exact trigger and eligible targets;
3. resource or state transitions;
4. proposed baseline values, units, caps, and cooldowns;
5. PvE, PvP, duel, siege, boss, and safe-zone behavior;
6. interaction with death, disconnect, restart, forms, mounts, and equipment changes;
7. ownership of every effect and persisted field;
8. configuration keys and validation ranges;
9. player and administrator feedback;
10. removal, respec, migration, and rollback behavior;
11. compatibility risks and required runtime evidence;
12. functional, balance, abuse, and recovery test cases.

## Resource-state template

Class resources must specify the complete state machine:

```text
Empty -> Generate -> Ready -> Spend -> Cooldown/Decay -> Empty
```

For each transition, document the event, amount, frequency limit, target rules, combat-state requirement, boss normalization, party attribution, and what happens if the triggering entity disappears.

Resource generation must have a server-configurable per-second or per-event cap. Bosses, summons, damage-over-time ticks, multi-hit abilities, friendly targets, destructibles, and repeated event delivery must not create unbounded generation.

## Tuning vocabulary

Use precise terms:

- **base value** — value before progression or synergy;
- **cap** — absolute maximum after all modifiers unless explicitly stated otherwise;
- **rate** — amount per second or per eligible event;
- **duration** — server-authoritative lifetime in seconds;
- **cooldown** — minimum time before reuse;
- **proc chance** — probability per eligible event, with an explicit frequency limit;
- **additive/multiplicative** — state the operation and order;
- **current/maximum blood or health** — never use “a portion” without identifying which.

Words such as “small,” “brief,” “improved,” or “reduced” may describe a concept, but a specification must pair them with proposed numeric values.

## Balance envelope

- Bloodlines should shift playstyle rather than supply unconditional best-in-slot power.
- Weaknesses must remain relevant in ordinary play and cannot be trivially canceled by the same bloodline.
- Soft synergies may alter behavior or efficiency, but should target no more than a modest advantage over a neutral pairing.
- Expansion classes should spend power on their distinctive loop rather than stacking free damage on top of Bloodcraft progression.
- PvP-impacting control, mobility, sustain, stealth, and burst require independent values or an explicit disabled state.
- Boss resource generation must be normalized against target durability rather than raw hit frequency.

Exact launch budgets will be established through named prototypes and recorded playtests; they should not be inferred from this concept document.

## Interaction matrix

Each release candidate must cover:

- every shipped bloodline × expansion-class pairing;
- the six upstream Bloodcraft classes;
- expertise and legacy stat categories;
- familiar present/absent states;
- solo, party, clan, duel, open PvP, and siege contexts that the server supports;
- supported and intentionally incompatible mods.

## Player experience and accessibility

- Selection and respec screens/messages must show strengths, weaknesses, costs, and permanence before confirmation.
- Important state cannot rely on color alone.
- Resource feedback needs a concise chat fallback when no client UI is installed.
- Names and descriptions must be localizable and must not embed logic.
- Error messages should explain the failed condition and the next valid action.
- Admin diagnostics must use stable IDs alongside display names.

## Playtest evidence

A controlled playtest report should record dependency versions, configuration, player count, encounter set, class uptime, resource generation/spending, damage and survival outliers, dominant pairings, failure cases, and tester observations. A concept advances only when its open risks are either resolved or published as explicit limitations.

---

[Expansion Hub](README.md) · [Architecture](ARCHITECTURE.md) · [Roadmap](ROADMAP.md)
