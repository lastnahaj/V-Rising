# BloodcraftExpansion Roadmap

!!! expansion "Custom / Planned Content"
    Nothing on this page claims an implemented BloodcraftExpansion gameplay feature.

| Phase | Goal | Exit condition |
|---|---|---|
| Foundation | Define data ownership, configuration, persistence, and compatibility boundaries | Architecture reviewed against named Bloodcraft and game versions |
| First playable content | Implement a minimal bloodline/class vertical slice | Server restart, reset, migration, and balance tests pass |
| Full roster | Add remaining bloodlines, classes, and specialization branches | Every effect has source ownership, caps, and admin controls |
| Content integration | Connect encounters, relics, factions, events, bounties, and dungeons | Each category has a tested gameplay loop and recovery plan |

## Architecture concerns to resolve

- Bloodcraft has no documented public expansion API in the stable source reviewed for this wiki.
- Custom class names overlap conceptually with upstream class selection and Shift-slot state.
- Persistence must not overwrite or reinterpret upstream JSON.
- Eclipse presentation requires a verified bridge; no custom protocol should be invented.
- Game-version PrefabGUIDs and hooks must be discovered and validated during implementation.

Future category stubs describe design goals only. See [Sources](../SOURCES.md) for the upstream version boundary.

---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
