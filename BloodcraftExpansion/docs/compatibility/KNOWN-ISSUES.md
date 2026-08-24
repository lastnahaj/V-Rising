# Known Issues and Warnings

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

| Area | Status | Upstream evidence |
|---|---|---|
| Familiar battles | Known issue / experimental | Config describes the system as most likely not working after V Rising 1.1 |
| `AllowMinions` | Experimental | Setting says to enable at own risk |
| Bleeding Edge | Experimental | Upstream calls the weapon changes experimental to varying degrees |
| Primal Arsenal | Experimental | Configuration explicitly labels it experimental |
| Elite Primal Rifts | WIP / optional | Upstream endgame section is WIP and the feature defaults off |
| Elite Shard Bearers | WIP / optional | Upstream endgame section is WIP and the feature defaults off |
| Exo forms | Experimental balance | Upstream describes Evolved Vampire and Corrupted Serpent as potentially unbalanced or janky |
| Familiar inventory contents | Known limitation | Equipment persists, but carried inventory may not persist across restarts and can drop on death/destruction |
| Restart scaling | Stable-version risk | Bloodcraft 1.13.26 prerelease notes fix duplicated Nightmare/Primal scaling across restarts; 1.13.22 predates that fix |

Primal and Nightmare systems modify combat globally. Test upgrades and restarts against a backup before exposing them to production players.

---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
