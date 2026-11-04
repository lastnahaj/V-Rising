# Sources, Versions, and Evidence Policy

This community wiki summarizes primary upstream material. It does not replace the Bloodcraft or Eclipse release notes, generated configuration, or in-game command output.

## Verified release baseline

| Component | Documented release | Source revision | Verification |
|---|---:|---|---|
| Bloodcraft | `1.13.26` | [`d44ede036fc5`](https://github.com/mfoltz/Bloodcraft/commit/d44ede036fc5f150b1aec8e65471b035bd2c9668) | Current Thunderstore package and tagged source checked 2026-11-02 |
| Eclipse | `1.3.14` | [`cbcdf58ff580`](https://github.com/mfoltz/Eclipse/commit/cbcdf58ff580e1f686334ff04ee7ef3998002358) | Current Thunderstore client package checked 2026-11-02 |
| V Rising references | `1.1.12-r99041-b2` | Bloodcraft project reference | Compile target only; not a blanket runtime compatibility guarantee |

Bloodcraft uses GitHub prerelease tags as release artifacts even when the matching version is published as the current Thunderstore package. For this wiki, **stable** means the latest version distributed through Thunderstore, not GitHub's release-label wording.

## Primary sources

- [Bloodcraft repository](https://github.com/mfoltz/Bloodcraft)
- [Bloodcraft 1.13.26 source tag](https://github.com/mfoltz/Bloodcraft/tree/v1.13.26-pre)
- [Bloodcraft 1.13.26 changelog](https://github.com/mfoltz/Bloodcraft/blob/v1.13.26-pre/CHANGELOG.md)
- [Bloodcraft Thunderstore package](https://thunderstore.io/c/v-rising/p/zfolmt/Bloodcraft/)
- [Bloodcraft Thunderstore versions](https://thunderstore.io/c/v-rising/p/zfolmt/Bloodcraft/versions/)
- [Eclipse repository](https://github.com/mfoltz/Eclipse)
- [Eclipse Thunderstore package](https://thunderstore.io/c/v-rising/p/zfolmt/Eclipse/)

## Evidence hierarchy

When sources disagree, this wiki uses the following order:

1. behavior in the tagged source for the documented package;
2. command attributes and configuration declarations in that source;
3. the package changelog and generated README;
4. live in-game output from an unmodified matching installation;
5. clearly labeled editorial guidance.

Command syntax is derived from the 88 `Command` attributes in the 1.13.26 source. Configuration names, types, defaults, and sections are derived from the 153 entries in `ConfigService.ConfigInitialization.ConfigEntries`.

## Recorded upstream discrepancies

| Area | Source behavior | Conflicting text | Wiki treatment |
|---|---|---|---|
| Quest rerolls | The command handles both Daily and Weekly and uses their separate configured costs. | Its command description still says “daily only currently.” | Both supported paths are documented, with the completed-weekly restriction. |
| Configuration count | Tagged source defines 153 entries. | The generated README can omit `UseEmberglassEclipseBridge`. | The source-defined 153-entry inventory is authoritative. |
| GitHub release label | The matching tag is marked prerelease. | Thunderstore publishes `1.13.26` as the current package. | Distribution status follows Thunderstore; source identity follows the exact tag and commit. |

## Scope limits

- PrefabGUID values appear only when the tagged source exposes them or assigns an upstream meaning.
- Unknown or server-replaced PrefabGUIDs remain numeric rather than receiving guessed friendly names.
- Editorial role summaries and configuration recommendations are labeled as community guidance.
- BloodcraftExpansion pages carry a persistent custom-content notice.
- Runtime compatibility must be rechecked after V Rising, Bloodcraft, Eclipse, BepInEx, or VampireCommandFramework updates.

## Maintenance policy

A version refresh is complete only when maintainers verify the package version, source revision, dependencies, command inventory, configuration inventory, migration concerns, and known-issue status together. Updating only the version banner is not sufficient.

---

[Wiki Home](HOME.md) · [Commands](reference/COMMANDS.md) · [Configuration](reference/CONFIGURATION.md)
