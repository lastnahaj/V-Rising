# Troubleshooting

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.26 stable<br>
    **Last verified:** 2026-11-02<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

## Bloodcraft does not load

- Confirm this is a V Rising dedicated server process; Bloodcraft exits outside the server role.
- Confirm stable dependencies are present: BepInExPack V Rising 1.733.2 and VampireCommandFramework 0.10.4 for Bloodcraft 1.13.26.
- Check the BepInEx log for the first Bloodcraft exception.
- Keep the Bloodcraft package, documentation baseline, BepInEx dependencies, and backups on a recorded matching version.

## A system or command is unavailable

Check its system enable flag and restart after configuration changes. Verify exact command syntax in the [source-derived table](../reference/COMMANDS.md) and confirm the caller has admin access where required.

## Eclipse UI is missing or stale

Bloodcraft works server-only; Eclipse is optional and must be installed on the client. Verify Bloodcraft/Eclipse versions, that the relevant server systems are enabled, and that client and server `Eclipsed` frequency settings are compatible.

## Progression looks duplicated after a restart

Bloodcraft 1.13.26 includes the persisted-unit guard for duplicate Nightmare/Primal scaling. If duplicate scaling is observed on 1.13.26, capture the exact package version, server log, mod list, affected unit, restart sequence, and save state before changing anything. Disable the affected option on a test copy and restore production only from a known-good backup.

## Familiar behavior

`AllowMinions` is explicitly use-at-own-risk. Familiar battles are documented upstream as likely not working after V Rising 1.1. Treat both as experimental.

---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
