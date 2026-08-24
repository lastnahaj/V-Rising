# Sources and Version Policy

This wiki summarizes primary upstream material rather than copying large sections verbatim.

## Verified snapshot

| Component | Documented release | Source revision | Status |
|---|---|---|---|
| Bloodcraft | `1.13.22` | [`cc34b357e6c3`](https://github.com/mfoltz/Bloodcraft/commit/cc34b357e6c3c88e5feadb9b0f7e11ab8d0c904f) | Current stable Thunderstore package checked 2026-08-23 |
| Bloodcraft prerelease | `1.13.26` | [`d44ede036fc5`](https://github.com/mfoltz/Bloodcraft/commit/d44ede036fc5f150b1aec8e65471b035bd2c9668) | GitHub prerelease; excluded from stable behavior except clearly labeled notes |
| Eclipse | `1.3.14` | [`cbcdf58ff580`](https://github.com/mfoltz/Eclipse/commit/cbcdf58ff580e1f686334ff04ee7ef3998002358) | Current stable Thunderstore client package checked 2026-08-23 |
| V Rising references | `1.1.12-r99041-b2` | Bloodcraft project reference | Build target, not a broad compatibility guarantee |

## Primary sources

- [Bloodcraft repository](https://github.com/mfoltz/Bloodcraft)
- [Bloodcraft 1.13.22 source tag](https://github.com/mfoltz/Bloodcraft/tree/v1.13.22-pre)
- [Bloodcraft changelog](https://github.com/mfoltz/Bloodcraft/blob/v1.13.22-pre/CHANGELOG.md)
- [Bloodcraft Thunderstore package](https://thunderstore.io/c/v-rising/p/zfolmt/Bloodcraft/)
- [Bloodcraft Thunderstore versions](https://thunderstore.io/c/v-rising/p/zfolmt/Bloodcraft/versions/)
- [Eclipse repository](https://github.com/mfoltz/Eclipse)
- [Eclipse Thunderstore package](https://thunderstore.io/c/v-rising/p/zfolmt/Eclipse/)

## How conflicts were resolved

Command syntax was taken from the `Command` attributes in the 1.13.22 source. Configuration keys and defaults were taken from `ConfigService.ConfigInitialization.ConfigEntries`. The generated upstream README contains 152 settings but omits `UseEmberglassEclipseBridge`; source defines 153, so this wiki follows source and records the discrepancy here.

The Thunderstore package is treated as stable distribution. GitHub releases use prerelease tags as publishing artifacts; versions newer than Thunderstore stable are not folded into stable instructions. The 1.13.26 fix for repeated Nightmare/Primal scaling is therefore documented as a prerelease fix and a stable-version warning.

## Scope limits

- PrefabGUID values are reproduced only where the stable source exposes them as configuration defaults.
- Friendly meanings are not assigned to unknown PrefabGUIDs.
- Editorial class roles are labeled as wiki summaries.
- Configuration examples are community suggestions, never upstream defaults.
- BloodcraftExpansion pages carry a custom-content notice.
---

[Wiki Home](HOME.md) · [Commands](reference/COMMANDS.md) · [Configuration](reference/CONFIGURATION.md)
