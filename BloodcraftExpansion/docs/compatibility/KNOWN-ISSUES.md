# Known Issues and Warnings

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.26 stable<br>
    **Last verified:** 2026-11-02<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

| Area | Status | Upstream evidence |
|---|---|---|
| Familiar battles | Known issue / experimental | Config describes the system as most likely not working after V Rising 1.1 |
| `AllowMinions` | Experimental | Setting says to enable at own risk |
| Bleeding Edge | Experimental | Upstream calls the weapon changes experimental to varying degrees |
| Primal Arsenal | Experimental | Configuration explicitly labels it experimental |
| Elite Primal Rifts | WIP / optional | Upstream endgame section is WIP and the feature defaults off |
| Elite Shard Bearers | WIP / optional | Upstream endgame section is WIP and the feature defaults off |
| Exo forms | Experimental balance | Upstream describes Evolved Vampire and Corrupted Serpent as potentially unbalanced and still carrying rough behavior |
| Familiar inventory contents | Known limitation | Equipment persists, but carried inventory may not persist across restarts and can drop on death/destruction |
| Restart scaling | Fixed in 1.13.26 | Persisted Nightmare and Primal Rift units are guarded against duplicate stat scaling across restarts. Re-test after any downgrade or version change. |

Primal and Nightmare systems modify combat globally. The known duplicate-scaling defect is fixed in 1.13.26, but upgrades, downgrades, and restarts should still be tested against a backup before production use.

---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
