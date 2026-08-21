using System.Collections.Immutable;

namespace SanguineRelay.State;

internal sealed record ServerSnapshot(
    bool IsOnline,
    string ServerName,
    string PublicAddress,
    int MaximumPlayers,
    DateTimeOffset? StartedAtUtc,
    DateTimeOffset? ResetAtUtc,
    string GameVersion,
    ImmutableArray<PlayerSnapshot> Players)
{
    public int OnlinePlayers => Players.Length;

    public TimeSpan Uptime => StartedAtUtc.HasValue
        ? DateTimeOffset.UtcNow - StartedAtUtc.Value
        : TimeSpan.Zero;

    public int DaysRunning
    {
        get
        {
            var origin = ResetAtUtc ?? StartedAtUtc;
            return origin.HasValue
                ? Math.Max(0, (int)Math.Floor((DateTimeOffset.UtcNow - origin.Value).TotalDays))
                : 0;
        }
    }
}
