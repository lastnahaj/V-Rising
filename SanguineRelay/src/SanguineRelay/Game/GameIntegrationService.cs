using BepInEx.Logging;
using ProjectM;
using ProjectM.CastleBuilding;
using ProjectM.Network;
using ProjectM.Scripting;
using Stunlock.Core;
using Unity.Collections;
using Unity.Entities;
using SanguineRelay.Core;
using SanguineRelay.State;

namespace SanguineRelay.Game;

internal sealed class GameIntegrationService
{
    private static readonly HashSet<Stunlock.Network.ConnectionStatusChangeReason> NonGameplayDisconnectReasons = new()
    {
        Stunlock.Network.ConnectionStatusChangeReason.IncorrectPassword,
        Stunlock.Network.ConnectionStatusChangeReason.ServerFull,
        Stunlock.Network.ConnectionStatusChangeReason.Unknown,
        Stunlock.Network.ConnectionStatusChangeReason.AuthenticationError,
        Stunlock.Network.ConnectionStatusChangeReason.AuthSessionCancelled
    };

    private sealed record PendingVBlood(string Boss, Dictionary<ulong, PlayerSnapshot> Players, DateTimeOffset LastSeenUtc);

    private readonly RelayOptions _options;
    private readonly ServerStateService _state;
    private readonly GameThreadDispatcher _dispatcher;
    private readonly ManualLogSource _log;
    private readonly Dictionary<int, PendingVBlood> _pendingVBloods = new();
    private World? _world;
    private PrefabCollectionSystem? _prefabs;
    private DateTimeOffset _nextReconciliationUtc;
    private bool _initialized;

    public GameIntegrationService(
        RelayOptions options,
        ServerStateService state,
        GameThreadDispatcher dispatcher,
        ManualLogSource log)
    {
        _options = options;
        _state = state;
        _dispatcher = dispatcher;
        _log = log;
    }

    public event Action<GameChatMessage>? GameChatReceived;
    public event Action<PlayerSnapshot>? PlayerJoined;
    public event Action<PlayerSnapshot>? PlayerLeft;
    public event Action<PlayerDeathEvent>? PlayerDied;
    public event Action<PvpKillEvent>? PvpKill;
    public event Action<VBloodKillEvent>? VBloodKill;
    public event Action<CastleBreachEvent>? CastleBreached;
    public event Action? ServerOnline;

    public bool IsInitialized => _initialized;

    public void Tick(ServerBootstrapSystem bootstrap)
    {
        _dispatcher.Drain();
        if (!_initialized)
        {
            Initialize(bootstrap);
        }

        var now = DateTimeOffset.UtcNow;
        if (now >= _nextReconciliationUtc)
        {
            ReconcilePlayers();
            _nextReconciliationUtc = now.AddSeconds(30);
        }

        FlushVBloodEvents(now);
    }

    public void HandlePlayerConnected(ServerBootstrapSystem bootstrap, Stunlock.Network.NetConnectionId connectionId)
    {
        EnsureInitialized(bootstrap);
        try
        {
            if (!bootstrap._NetEndPointToApprovedUserIndex.TryGetValue(connectionId, out var index) ||
                index < 0 || index >= bootstrap._ApprovedUsersLookup.Count)
            {
                return;
            }

            var client = bootstrap._ApprovedUsersLookup[index];
            if (client is null || client.UserEntity == Entity.Null || !TryCreatePlayer(client.UserEntity, out var player))
            {
                return;
            }

            var wasOnline = _state.Current.Players.Any(existing => existing.PlatformId == player.PlatformId);
            _state.UpsertPlayer(player);
            if (!wasOnline)
            {
                PlayerJoined?.Invoke(player);
            }
        }
        catch (Exception exception)
        {
            _log.LogError($"Player connection processing failed: {exception.GetType().Name}: {exception.Message}");
        }
    }

    public void HandlePlayerDisconnected(
        ServerBootstrapSystem bootstrap,
        Stunlock.Network.NetConnectionId connectionId,
        Stunlock.Network.ConnectionStatusChangeReason reason)
    {
        EnsureInitialized(bootstrap);
        if (NonGameplayDisconnectReasons.Contains(reason))
        {
            return;
        }

        try
        {
            if (!bootstrap._NetEndPointToApprovedUserIndex.TryGetValue(connectionId, out var index) ||
                index < 0 || index >= bootstrap._ApprovedUsersLookup.Count)
            {
                return;
            }

            var client = bootstrap._ApprovedUsersLookup[index];
            if (client is null || client.UserEntity == Entity.Null || !TryCreatePlayer(client.UserEntity, out var player))
            {
                return;
            }

            var removed = _state.RemovePlayer(player.PlatformId);
            if (removed is not null)
            {
                PlayerLeft?.Invoke(removed);
            }
        }
        catch (Exception exception)
        {
            _log.LogError($"Player disconnection processing failed: {exception.GetType().Name}: {exception.Message}");
        }
    }

    public void HandleChat(ChatMessageSystem system)
    {
        if (!_initialized || _world is null)
        {
            return;
        }

        var entities = system.__query_661171423_0.ToEntityArray(Allocator.Temp);
        try
        {
            foreach (var entity in entities)
            {
                if (!_world.EntityManager.HasComponent<ChatMessageEvent>(entity) ||
                    !_world.EntityManager.HasComponent<FromCharacter>(entity))
                {
                    continue;
                }

                var chat = _world.EntityManager.GetComponentData<ChatMessageEvent>(entity);
                var from = _world.EntityManager.GetComponentData<FromCharacter>(entity);
                if (!TryCreatePlayer(from.User, out var player))
                {
                    continue;
                }

                var channel = chat.MessageType.ToString();
                if (!ShouldRelayChannel(channel))
                {
                    continue;
                }

                var message = chat.MessageText.Value?.Trim();
                if (!string.IsNullOrWhiteSpace(message))
                {
                    GameChatReceived?.Invoke(new GameChatMessage(player, message, channel));
                }
            }
        }
        catch (Exception exception)
        {
            _log.LogError($"Game chat processing failed: {exception.GetType().Name}: {exception.Message}");
        }
        finally
        {
            entities.Dispose();
        }
    }

    public void HandleDeathEvents(DeathEventListenerSystem system)
    {
        if (!_initialized || _world is null)
        {
            return;
        }

        var events = system._DeathEventQuery.ToComponentDataArray<DeathEvent>(Allocator.Temp);
        try
        {
            foreach (var death in events)
            {
                if (_options.Events.CastleRaids)
                {
                    TryPublishCastleBreach(death);
                }

                if (!_options.Events.PlayerDeaths ||
                    !TryCreatePlayerFromCharacter(death.Died, out var victim) ||
                    TryCreatePlayerFromCharacter(death.Killer, out _))
                {
                    continue;
                }

                PlayerDied?.Invoke(new PlayerDeathEvent(victim));
            }
        }
        catch (Exception exception)
        {
            _log.LogError($"Death event processing failed: {exception.GetType().Name}: {exception.Message}");
        }
        finally
        {
            events.Dispose();
        }
    }

    public void HandlePvpDowned(VampireDownedServerEventSystem system)
    {
        if (!_initialized || _world is null || !_options.Events.PvpKills)
        {
            return;
        }

        var entities = system.__query_1174204813_0.ToEntityArray(Allocator.Temp);
        try
        {
            foreach (var eventEntity in entities)
            {
                VampireDownedServerEventSystem.TryFindRootOwner(eventEntity, 1, _world.EntityManager, out var victimEntity);
                if (!_world.EntityManager.TryGetComponentData<VampireDownedBuff>(eventEntity, out var buff))
                {
                    continue;
                }

                VampireDownedServerEventSystem.TryFindRootOwner(buff.Source, 1, _world.EntityManager, out var killerEntity);
                if (killerEntity == victimEntity ||
                    !TryCreatePlayerFromCharacter(killerEntity, out var killer) ||
                    !TryCreatePlayerFromCharacter(victimEntity, out var victim))
                {
                    continue;
                }

                PvpKill?.Invoke(new PvpKillEvent(killer, victim));
            }
        }
        catch (Exception exception)
        {
            _log.LogError($"PvP event processing failed: {exception.GetType().Name}: {exception.Message}");
        }
        finally
        {
            entities.Dispose();
        }
    }

    public void HandleVBlood(VBloodSystem system)
    {
        if (!_initialized || _world is null || !_options.Events.VBloodKills)
        {
            return;
        }

        try
        {
            foreach (var consumed in system.EventList)
            {
                if (!TryCreatePlayerFromCharacter(consumed.Target, out var player))
                {
                    continue;
                }

                var bossId = consumed.Source.GuidHash;
                var boss = ResolvePrefabName(consumed.Source);
                if (!_pendingVBloods.TryGetValue(bossId, out var pending))
                {
                    pending = new PendingVBlood(boss, new Dictionary<ulong, PlayerSnapshot>(), DateTimeOffset.UtcNow);
                }

                pending.Players[player.PlatformId] = player;
                _pendingVBloods[bossId] = pending with { LastSeenUtc = DateTimeOffset.UtcNow };
            }
        }
        catch (Exception exception)
        {
            _log.LogError($"V Blood event processing failed: {exception.GetType().Name}: {exception.Message}");
        }
    }

    public void SendSystemMessage(string message)
    {
        if (!_initialized || _world is null)
        {
            throw new InvalidOperationException("The V Rising server world is not initialized.");
        }

        var bytes = new FixedString512Bytes(TextSanitizer.Truncate(message, 500));
        ServerChatUtils.SendSystemMessageToAllClients(_world.EntityManager, ref bytes);
    }

    private void Initialize(ServerBootstrapSystem bootstrap)
    {
        _world = bootstrap.World;
        _prefabs = _world.GetExistingSystemManaged<PrefabCollectionSystem>();
        _initialized = true;
        _nextReconciliationUtc = DateTimeOffset.MinValue;

        var configuredName = _options.ServerInfo.DisplayName;
        var gameName = SettingsManager.ServerHostSettings.Name ?? string.Empty;
        var serverName = string.IsNullOrWhiteSpace(configuredName) ? gameName : configuredName;
        var maximumPlayers = SettingsManager.ServerHostSettings.MaxConnectedUsers;
        var version = typeof(ServerBootstrapSystem).Assembly.GetName().Version?.ToString() ?? "1.1.x";
        _state.SetServerOnline(serverName, maximumPlayers, version);
        ReconcilePlayers();
        ServerOnline?.Invoke();
        _log.LogInfo("V Rising server state initialized.");
    }

    private void EnsureInitialized(ServerBootstrapSystem bootstrap)
    {
        if (!_initialized)
        {
            Initialize(bootstrap);
        }
    }

    private void ReconcilePlayers()
    {
        if (_world is null)
        {
            return;
        }

        var queryBuilder = new EntityQueryBuilder(Allocator.Temp);
        queryBuilder.AddAll(ComponentType.ReadOnly<User>());
        queryBuilder.WithOptions(EntityQueryOptions.IncludeDisabled);
        var query = _world.EntityManager.CreateEntityQuery(ref queryBuilder);
        var entities = query.ToEntityArray(Allocator.Temp);
        try
        {
            var players = new List<PlayerSnapshot>();
            foreach (var entity in entities)
            {
                var user = _world.EntityManager.GetComponentData<User>(entity);
                if (user.IsConnected && TryCreatePlayer(entity, out var player))
                {
                    players.Add(player);
                }
            }

            _state.Reconcile(players);
        }
        catch (Exception exception)
        {
            _log.LogWarning($"Player-state reconciliation failed: {exception.GetType().Name}: {exception.Message}");
        }
        finally
        {
            entities.Dispose();
            query.Dispose();
            queryBuilder.Dispose();
        }
    }

    private bool TryCreatePlayer(Entity userEntity, out PlayerSnapshot player)
    {
        player = null!;
        if (_world is null || userEntity == Entity.Null ||
            !_world.EntityManager.Exists(userEntity) ||
            !_world.EntityManager.HasComponent<User>(userEntity))
        {
            return false;
        }

        var user = _world.EntityManager.GetComponentData<User>(userEntity);
        var name = user.CharacterName.Value?.Trim();
        if (user.PlatformId == 0 || string.IsNullOrWhiteSpace(name) || name.StartsWith("[NPC]", StringComparison.Ordinal))
        {
            return false;
        }

        string? clan = null;
        var clanEntity = user.ClanEntity._Entity;
        if (clanEntity != Entity.Null && _world.EntityManager.Exists(clanEntity) &&
            _world.EntityManager.HasComponent<ClanTeam>(clanEntity))
        {
            clan = _world.EntityManager.GetComponentData<ClanTeam>(clanEntity).Name.ToString();
        }

        int? gearLevel = null;
        var characterEntity = user.LocalCharacter._Entity;
        if (characterEntity != Entity.Null && _world.EntityManager.Exists(characterEntity) &&
            _world.EntityManager.HasComponent<Equipment>(characterEntity))
        {
            gearLevel = Convert.ToInt32(_world.EntityManager.GetComponentData<Equipment>(characterEntity).GetFullLevel());
        }

        player = new PlayerSnapshot(user.PlatformId, name, clan, gearLevel, DateTimeOffset.UtcNow);
        return true;
    }

    private bool TryCreatePlayerFromCharacter(Entity characterEntity, out PlayerSnapshot player)
    {
        player = null!;
        if (_world is null || characterEntity == Entity.Null || !_world.EntityManager.Exists(characterEntity) ||
            !_world.EntityManager.HasComponent<PlayerCharacter>(characterEntity))
        {
            return false;
        }

        var character = _world.EntityManager.GetComponentData<PlayerCharacter>(characterEntity);
        return TryCreatePlayer(character.UserEntity, out player);
    }

    private bool ShouldRelayChannel(string channel) =>
        (_options.Chat.RelayGlobal && channel.Contains("Global", StringComparison.OrdinalIgnoreCase)) ||
        (_options.Chat.RelayLocal && (channel.Contains("Local", StringComparison.OrdinalIgnoreCase) || channel.Contains("Region", StringComparison.OrdinalIgnoreCase))) ||
        (_options.Chat.RelayClan && channel.Contains("Clan", StringComparison.OrdinalIgnoreCase));

    private void FlushVBloodEvents(DateTimeOffset now)
    {
        if (_pendingVBloods.Count == 0)
        {
            return;
        }

        var ready = _pendingVBloods
            .Where(pair => now - pair.Value.LastSeenUtc >= TimeSpan.FromSeconds(2))
            .Select(pair => pair.Key)
            .ToArray();

        foreach (var id in ready)
        {
            var pending = _pendingVBloods[id];
            _pendingVBloods.Remove(id);
            VBloodKill?.Invoke(new VBloodKillEvent(pending.Players.Values.ToArray(), pending.Boss));
        }
    }

    private string ResolvePrefabName(PrefabGUID prefab)
    {
        if (_prefabs is not null && _prefabs._PrefabLookupMap.TryGetName(prefab, out var name))
        {
            var value = name.ToString();
            var segments = value.Split('_', StringSplitOptions.RemoveEmptyEntries);
            var useful = segments.Where(segment =>
                !segment.Equals("CHAR", StringComparison.OrdinalIgnoreCase) &&
                !segment.Equals("VBlood", StringComparison.OrdinalIgnoreCase) &&
                !segment.StartsWith("VBlood", StringComparison.OrdinalIgnoreCase) &&
                !segment.StartsWith("UNIQUE", StringComparison.OrdinalIgnoreCase));
            var readable = string.Join(' ', useful);
            if (!string.IsNullOrWhiteSpace(readable))
            {
                return readable;
            }
        }

        return $"V Blood {prefab.GuidHash}";
    }

    private void TryPublishCastleBreach(DeathEvent death)
    {
        if (_world is null || death.Died == Entity.Null || !_world.EntityManager.Exists(death.Died) ||
            !_world.EntityManager.HasComponent<AnnounceCastleBreached>(death.Died) ||
            !_world.EntityManager.HasComponent<CastleHeartConnection>(death.Died) ||
            !TryCreatePlayerFromCharacter(death.Killer, out var attacker))
        {
            return;
        }

        var heart = _world.EntityManager.GetComponentData<CastleHeartConnection>(death.Died).CastleHeartEntity._Entity;
        if (heart == Entity.Null || !_world.EntityManager.Exists(heart) || !_world.EntityManager.HasComponent<UserOwner>(heart))
        {
            return;
        }

        var ownerEntity = _world.EntityManager.GetComponentData<UserOwner>(heart).Owner._Entity;
        if (TryCreatePlayer(ownerEntity, out var owner))
        {
            CastleBreached?.Invoke(new CastleBreachEvent(attacker, owner));
        }
    }
}
