namespace SanguineRelay.Discord;

internal enum AuditOutcome
{
    Success,
    Denied,
    ValidationFailure,
    Timeout,
    ExecutionFailure
}

internal sealed record AuditMetrics(long Queued, long Dropped, long DeliveryFailures);

internal sealed class AuditHealth
{
    private long _queued;
    private long _dropped;
    private long _deliveryFailures;

    public AuditMetrics Metrics => new(
        Interlocked.Read(ref _queued),
        Interlocked.Read(ref _dropped),
        Interlocked.Read(ref _deliveryFailures));

    public void RecordQueued() => Interlocked.Increment(ref _queued);

    public void RecordDropped() => Interlocked.Increment(ref _dropped);

    public void RecordDeliveryFailure() => Interlocked.Increment(ref _deliveryFailures);
}

internal static class DiscordAuditPolicy
{
    private static readonly IReadOnlySet<string> AuditedCommands = new HashSet<string>(StringComparer.Ordinal)
    {
        "player",
        "announce",
        "relay-status"
    };

    public static bool RequiresAudit(string commandName) => AuditedCommands.Contains(commandName);

    public static string Describe(AuditOutcome outcome, string? detail = null)
    {
        var result = outcome switch
        {
            AuditOutcome.Success => "Success",
            AuditOutcome.Denied => "Denied",
            AuditOutcome.ValidationFailure => "Validation failure",
            AuditOutcome.Timeout => "Timed out",
            AuditOutcome.ExecutionFailure => "Execution failure",
            _ => throw new ArgumentOutOfRangeException(nameof(outcome))
        };
        return string.IsNullOrWhiteSpace(detail) ? result : $"{result} ({detail})";
    }
}
