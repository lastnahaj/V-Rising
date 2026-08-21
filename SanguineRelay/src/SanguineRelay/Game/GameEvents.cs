using SanguineRelay.State;

namespace SanguineRelay.Game;

internal sealed record GameChatMessage(PlayerSnapshot Player, string Message, string Channel);

internal sealed record PlayerDeathEvent(PlayerSnapshot Victim);

internal sealed record PvpKillEvent(PlayerSnapshot Killer, PlayerSnapshot Victim);

internal sealed record VBloodKillEvent(IReadOnlyList<PlayerSnapshot> Players, string Boss);

internal sealed record CastleBreachEvent(PlayerSnapshot Attacker, PlayerSnapshot Owner);
