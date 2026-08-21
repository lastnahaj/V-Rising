using SanguineRelay.State;

namespace SanguineRelay.Tests;

public sealed class ServerStateServiceTests
{
    [Fact]
    public void EveryConsumerSeesTheSameAuthoritativePlayerSnapshot()
    {
        var state = new ServerStateService("Vardoran", "play.example.com", null);
        var joinedAt = DateTimeOffset.UtcNow.AddMinutes(-10);
        state.UpsertPlayer(new PlayerSnapshot(1, "CrimsonWarden_47", "Nightfall", 91, joinedAt));
        state.UpsertPlayer(new PlayerSnapshot(1, "CrimsonWarden_47", "Nightfall", 92, DateTimeOffset.UtcNow));

        var snapshot = state.Current;

        Assert.Single(snapshot.Players);
        Assert.Equal(92, snapshot.Players[0].GearLevel);
        Assert.Equal(joinedAt, snapshot.Players[0].JoinedAtUtc);
        Assert.Same(snapshot, state.Current);
    }

    [Fact]
    public void ReconciliationPreservesExistingJoinOrder()
    {
        var state = new ServerStateService("Vardoran", string.Empty, null);
        var joinedAt = DateTimeOffset.UtcNow.AddHours(-1);
        state.UpsertPlayer(new PlayerSnapshot(1, "MoonlitRook_83", null, null, joinedAt));

        state.Reconcile(new[] { new PlayerSnapshot(1, "MoonlitRook_83", "Clan", 40, DateTimeOffset.UtcNow) });

        Assert.Equal(joinedAt, state.Current.Players[0].JoinedAtUtc);
    }

    [Fact]
    public void UptimeBeginsAtWorldInitializationAndDoesNotReset()
    {
        var now = new DateTimeOffset(2026, 8, 20, 12, 0, 0, TimeSpan.Zero);
        var state = new ServerStateService("Vardoran", string.Empty, null, () => now);

        Assert.Null(state.Current.StartedAtUtc);
        Assert.Equal(TimeSpan.Zero, state.Current.Uptime);

        state.SetServerOnline("Vardoran", 60, "1.1.13.0");
        var startedAt = state.Current.StartedAtUtc;
        now = now.AddHours(1);
        state.SetServerOnline("Vardoran", 60, "1.1.13.0");

        Assert.Equal(new DateTimeOffset(2026, 8, 20, 12, 0, 0, TimeSpan.Zero), startedAt);
        Assert.Equal(startedAt, state.Current.StartedAtUtc);
    }
}
