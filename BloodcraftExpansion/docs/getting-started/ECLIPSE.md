# Eclipse Client Companion

!!! version "Upstream Bloodcraft Reference"
    **Bloodcraft version:** 1.13.22 stable<br>
    **Last verified:** 2026-08-23<br>
    This page documents upstream Bloodcraft, not BloodcraftExpansion custom features.

Eclipse is the upstream-author’s client UI for Bloodcraft. Stable Eclipse `1.3.14` is optional: Bloodcraft remains a server mod and chat commands remain available without the client UI.

## What Eclipse adds

- progress bars and information for leveling, blood legacies, weapon expertise, familiars, professions, and quests;
- the active Shift-slot ability and cooldown when configured;
- prestige display;
- Bloodcraft stats in the Attributes tab, including clickable expertise/legacy selections;
- per-widget configuration and click toggles from the ability bar;
- a master UI toggle on the blood orb.

## Installation model

Each player who wants the UI installs Eclipse on their own client with BepInExPack for V Rising. Players without Eclipse can still use server-side Bloodcraft features through chat commands. The Bloodcraft server automatically activates its client bridge when a synchronizable feature is enabled; `Eclipsed` controls update frequency.

Bloodcraft 1.13.22 and Eclipse 1.3.14 also include optional Emberglass transport support. `UseEmberglassEclipseBridge=false` keeps the legacy chat-message bridge and falls back to it when Emberglass is unavailable.

See the full [Eclipse compatibility page](../compatibility/ECLIPSE.md) for UI settings and version notes.
---

[Wiki Home](../HOME.md) · [Commands](../reference/COMMANDS.md) · [Configuration](../reference/CONFIGURATION.md)
