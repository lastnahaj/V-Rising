using System.Collections.Immutable;

namespace SanguineRelay.State;

internal sealed class ServerStateService
{
    private readonly object _gate = new();
    private readonly Func<DateTimeOffset> _utcNow;
    private ServerSnapshot _current;

    public ServerStateService(
        string configuredName,
        string publicAddress,
        DateTimeOffset? resetAtUtc,
        Func<DateTimeOffset>? utcNow = null)
    {
        _utcNow = utcNow ?? (() => DateTimeOffset.UtcNow);
        _current = new ServerSnapshot(
            false,
            string.IsNullOrWhiteSpace(configuredName) ? "V Rising Server" : configuredName,
            publicAddress,
            0,
            null,
            resetAtUtc,
            "Unknown",
            ImmutableArray<PlayerSnapshot>.Empty);
    }

    public event Action<ServerSnapshot>? Changed;

    public ServerSnapshot Current
    {
        get
        {
            lock (_gate)
            {
                return _current;
            }
        }
    }

    public void SetServerOnline(string serverName, int maximumPlayers, string gameVersion)
    {
        Update(snapshot => snapshot with
        {
            IsOnline = true,
            StartedAtUtc = snapshot.StartedAtUtc ?? _utcNow(),
            ServerName = string.IsNullOrWhiteSpace(serverName) ? snapshot.ServerName : serverName,
            MaximumPlayers = Math.Max(0, maximumPlayers),
            GameVersion = string.IsNullOrWhiteSpace(gameVersion) ? snapshot.GameVersion : gameVersion
        });
    }

    public void SetServerOffline() => Update(snapshot => snapshot with { IsOnline = false });

    public void UpsertPlayer(PlayerSnapshot player)
    {
        Update(snapshot =>
        {
            var players = snapshot.Players.ToBuilder();
            var index = -1;
            for (var i = 0; i < players.Count; i++)
            {
                if (players[i].PlatformId == player.PlatformId)
                {
                    index = i;
                    break;
                }
            }

            if (index >= 0)
            {
                var existing = players[index];
                players[index] = player with { JoinedAtUtc = existing.JoinedAtUtc };
            }
            else
            {
                players.Add(player);
            }

            return snapshot with { Players = players.ToImmutable() };
        });
    }

    public PlayerSnapshot? RemovePlayer(ulong platformId)
    {
        PlayerSnapshot? removed = null;
        Update(snapshot =>
        {
            var players = snapshot.Players.ToBuilder();
            for (var i = 0; i < players.Count; i++)
            {
                if (players[i].PlatformId != platformId)
                {
                    continue;
                }

                removed = players[i];
                players.RemoveAt(i);
                break;
            }

            return snapshot with { Players = players.ToImmutable() };
        });
        return removed;
    }

    public void Reconcile(IEnumerable<PlayerSnapshot> players)
    {
        var now = DateTimeOffset.UtcNow;
        Update(snapshot =>
        {
            var joined = snapshot.Players.ToDictionary(player => player.PlatformId, player => player.JoinedAtUtc);
            var reconciled = players
                .Where(player => player.PlatformId != 0 && !string.IsNullOrWhiteSpace(player.Name))
                .GroupBy(player => player.PlatformId)
                .Select(group => group.Last() with
                {
                    JoinedAtUtc = joined.TryGetValue(group.Key, out var joinedAt) ? joinedAt : now
                })
                .ToImmutableArray();
            return snapshot with { Players = reconciled };
        });
    }

    public PlayerSnapshot? FindPlayer(string name)
    {
        var snapshot = Current;
        return snapshot.Players.FirstOrDefault(player =>
            player.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    private void Update(Func<ServerSnapshot, ServerSnapshot> update)
    {
        ServerSnapshot next;
        lock (_gate)
        {
            next = update(_current);
            if (ReferenceEquals(next, _current) || next == _current)
            {
                return;
            }

            _current = next;
        }

        Changed?.Invoke(next);
    }
}
