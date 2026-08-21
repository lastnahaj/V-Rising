namespace SanguineRelay.State;

internal sealed record PlayerSnapshot(
    ulong PlatformId,
    string Name,
    string? Clan,
    int? GearLevel,
    DateTimeOffset JoinedAtUtc);
