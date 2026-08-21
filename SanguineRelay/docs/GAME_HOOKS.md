# V Rising Hook Verification

Target: V Rising dedicated server 1.1.x, compiled against metadata-only `1.1.12-r99041-b2` references with BepInExPack V Rising 1.733.2.

| Feature | Hook or API | Compile verified | Runtime verified |
| --- | --- | --- | --- |
| Server initialization and dispatcher | `ProjectM.ServerBootstrapSystem.OnUpdate` | Yes | Pending |
| Player join | `ProjectM.ServerBootstrapSystem.OnUserConnected(NetConnectionId)` postfix | Yes | Pending |
| Player leave | `ProjectM.ServerBootstrapSystem.OnUserDisconnected(NetConnectionId, ConnectionStatusChangeReason, string)` prefix | Yes | Pending |
| Player reconciliation | `ProjectM.Network.User` entity query filtered by `User.IsConnected` | Yes | Pending |
| Receive game chat | `ProjectM.ChatMessageSystem.OnUpdate` prefix and chat event query | Yes | Pending |
| Send game chat | `ProjectM.ServerChatUtils.SendSystemMessageToAllClients` | Yes | Pending |
| Player death | `ProjectM.DeathEventListenerSystem.OnUpdate` prefix and `DeathEvent` | Yes | Pending |
| PvP downed | `ProjectM.VampireDownedServerEventSystem.OnUpdate` postfix | Yes | Pending |
| V Blood completion | `ProjectM.VBloodSystem.OnUpdate` prefix and `EventList` | Yes | Pending |
| Castle breach | `DeathEvent` with `AnnounceCastleBreached` and `CastleHeartConnection` | Yes | Pending |

## Patch rationale

### ServerBootstrapSystem.OnUpdate

The server bootstrap update is the stable game-thread boundary used to drain work queued by asynchronous Discord callbacks. The dispatcher has a fixed capacity of 1,024 operations, prompt caller cancellation, shutdown cancellation, and observable pending/rejected counters. It also performs a 30-second reconciliation of the one cached player snapshot. No Discord operation runs on this thread.

### ServerBootstrapSystem.OnUserConnected and OnUserDisconnected

Connection callbacks provide the approved user entity while its `ProjectM.Network.User` data is valid. The disconnect prefix captures the immutable player data before the game removes the connection mapping. These hooks update `ServerStateService`; downstream modules never enumerate player entities independently.

### ChatMessageSystem.OnUpdate

The prefix copies the sender identity, channel type, and message text from the current chat-event entities before the game consumes them. Only configured channel types are emitted. ECS entities are never retained after the call.

### DeathEventListenerSystem.OnUpdate

The prefix copies player death and castle-breach facts before the listener processes its event query. A death whose killer resolves to a player is omitted from the general death feed so the PvP feed remains authoritative for that event.

### VampireDownedServerEventSystem.OnUpdate

The postfix resolves root owners for the downed buff source and target. An event is emitted only when both resolve to distinct player characters. This is the maintained 1.1 pattern for PvP downed detection.

### VBloodSystem.OnUpdate

The prefix reads completed V Blood consumption events. Participants are copied into immutable player snapshots and grouped by boss for two seconds, preventing one shared encounter from being announced once per participant.

### Castle breach capture

Castle reporting requires the explicit `AnnounceCastleBreached` marker and a valid player attacker, castle-heart connection, and user owner. Coordinates are not read. The feature defaults to disabled until live validation is complete.

## Live verification procedure

On an isolated staging server:

1. Confirm the plugin loads with no Harmony patch errors.
2. Connect two ordinary unmodded clients and verify both appear in `/players`.
3. Exercise global chat in both directions and confirm no loop occurs.
4. Disconnect and reconnect within the suppression window, then outside it.
5. Perform a controlled PvP down and confirm one PvP event and no duplicate general death event.
6. Complete a V Blood encounter with multiple participants and confirm one grouped event.
7. If castle reporting is enabled, perform a controlled breach and confirm no location data appears.
8. Restart Discord connectivity and confirm the V Rising process remains healthy.
9. Delete the status message and verify configured recreation behavior.
10. Burst-connect several clients and verify a single final voice-counter rename.
11. Stop the server cleanly and verify the existing status embed, voice counter, presence, and lifecycle channel receive their final offline state within the bounded shutdown window.
12. Exercise `/player`, `/announce`, and `/relay-status` as allowed and denied users and verify every result reaches the private audit channel.

Change `Runtime verified` to `Yes` only after recording a successful test on the exact deployed V Rising build.
