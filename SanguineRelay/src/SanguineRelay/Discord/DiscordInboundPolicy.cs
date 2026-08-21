namespace SanguineRelay.Discord;

internal readonly record struct DiscordInboundMessageContext(
    ulong AuthorId,
    bool IsBot,
    bool IsOwnBot,
    bool IsWebhook,
    bool IsSystem);

internal static class DiscordInboundPolicy
{
    public static bool ShouldIgnore(
        DiscordInboundMessageContext message,
        bool ignoreAllBots,
        IReadOnlySet<ulong> ignoredUserIds,
        IReadOnlySet<ulong> ignoredBotIds) =>
        message.IsOwnBot ||
        message.IsWebhook ||
        message.IsSystem ||
        ignoredUserIds.Contains(message.AuthorId) ||
        message.IsBot && (ignoreAllBots || ignoredBotIds.Contains(message.AuthorId));
}
