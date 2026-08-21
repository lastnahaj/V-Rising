using Discord;
using SanguineRelay.Core;
using SanguineRelay.State;

namespace SanguineRelay.Discord;

internal static class DiscordEmbedFactory
{
    public static Embed BuildStatus(ServerSnapshot snapshot, StatusEmbedOptions options)
    {
        var values = Values(snapshot);
        var builder = new EmbedBuilder()
            .WithTitle(TextSanitizer.DiscordDisplay(TemplateFormatter.Format(options.TitleFormat, values), 256))
            .WithColor(new Color(snapshot.IsOnline ? options.OnlineColor : options.OfflineColor));

        if (options.ShowIpPort && !string.IsNullOrWhiteSpace(snapshot.PublicAddress))
        {
            builder.AddField(
                TextSanitizer.DiscordDisplay(options.IpPortLabel, 256),
                TextSanitizer.DiscordDisplay(snapshot.PublicAddress, 1024),
                options.InlineSummaryFields);
        }

        builder.AddField(
            TextSanitizer.DiscordDisplay(options.OnlineCountLabel, 256),
            $"{snapshot.OnlinePlayers}/{snapshot.MaximumPlayers}",
            options.InlineSummaryFields);

        if (options.ShowDaysRunning)
        {
            builder.AddField(
                TextSanitizer.DiscordDisplay(options.DaysRunningLabel, 256),
                snapshot.DaysRunning.ToString(System.Globalization.CultureInfo.InvariantCulture),
                options.InlineSummaryFields);
        }

        if (options.ShowPlayerList)
        {
            builder.AddField(
                TextSanitizer.DiscordDisplay(options.PlayersSectionTitle, 256),
                BuildPlayerList(snapshot, options),
                false);
        }

        if (options.ShowTimestamp)
        {
            builder.WithTimestamp(DateTimeOffset.UtcNow);
        }

        return builder.Build();
    }

    public static Embed BuildCommandStatus(ServerSnapshot snapshot)
    {
        var status = snapshot.IsOnline ? "Online" : "Offline";
        return new EmbedBuilder()
            .WithTitle(TextSanitizer.DiscordDisplay(snapshot.ServerName, 256))
            .WithColor(snapshot.IsOnline ? new Color(0x00A651) : new Color(0xD32F2F))
            .AddField("Status", status, true)
            .AddField("Players", $"{snapshot.OnlinePlayers}/{snapshot.MaximumPlayers}", true)
            .AddField("Uptime", FormatDuration(snapshot.Uptime), true)
            .AddField("Days running", snapshot.DaysRunning, true)
            .AddField("Game version", TextSanitizer.DiscordDisplay(snapshot.GameVersion, 1024), true)
            .WithTimestamp(DateTimeOffset.UtcNow)
            .Build();
    }

    public static Embed BuildPlayers(ServerSnapshot snapshot)
    {
        var names = snapshot.Players
            .OrderBy(player => player.Name, StringComparer.OrdinalIgnoreCase)
            .Select(player => TextSanitizer.DiscordDisplay(player.Name, 80));
        var list = string.Join('\n', names);
        if (list.Length == 0)
        {
            list = "No-one is online";
        }

        return new EmbedBuilder()
            .WithTitle($"{snapshot.OnlinePlayers}/{snapshot.MaximumPlayers} Online")
            .WithDescription(TextSanitizer.Truncate(list, 4096))
            .WithColor(snapshot.IsOnline ? new Color(0x00A651) : new Color(0xD32F2F))
            .Build();
    }

    public static IReadOnlyDictionary<string, string?> Values(ServerSnapshot snapshot) =>
        new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase)
        {
            ["server"] = snapshot.ServerName,
            ["online"] = snapshot.OnlinePlayers.ToString(System.Globalization.CultureInfo.InvariantCulture),
            ["max"] = snapshot.MaximumPlayers.ToString(System.Globalization.CultureInfo.InvariantCulture),
            ["ip"] = snapshot.PublicAddress,
            ["days"] = snapshot.DaysRunning.ToString(System.Globalization.CultureInfo.InvariantCulture),
            ["uptime"] = FormatDuration(snapshot.Uptime)
        };

    public static string FormatDuration(TimeSpan duration)
    {
        if (duration.TotalDays >= 1)
        {
            return $"{(int)duration.TotalDays}d {duration.Hours}h {duration.Minutes}m";
        }

        return duration.TotalHours >= 1
            ? $"{(int)duration.TotalHours}h {duration.Minutes}m"
            : $"{Math.Max(0, duration.Minutes)}m";
    }

    private static string BuildPlayerList(ServerSnapshot snapshot, StatusEmbedOptions options)
    {
        IEnumerable<PlayerSnapshot> players = snapshot.Players;
        if (options.PlayerSortMode.Equals("Alphabetical", StringComparison.OrdinalIgnoreCase))
        {
            players = players.OrderBy(player => player.Name, StringComparer.OrdinalIgnoreCase);
        }
        else if (options.PlayerSortMode.Equals("JoinOrder", StringComparison.OrdinalIgnoreCase))
        {
            players = players.OrderBy(player => player.JoinedAtUtc);
        }

        var all = players.ToArray();
        var shown = all.Take(options.MaxPlayersShown).Select(player => TextSanitizer.DiscordDisplay(player.Name, 80)).ToList();
        if (shown.Count == 0)
        {
            return TextSanitizer.DiscordDisplay(options.EmptyPlayersText, 1024);
        }

        if (all.Length > shown.Count)
        {
            shown.Add($"+ {all.Length - shown.Count} more players");
        }

        while (shown.Count > 1 && string.Join('\n', shown).Length > 1024)
        {
            shown.RemoveAt(shown.Count - 1);
            shown[^1] = $"+ {all.Length - shown.Count + 1} more players";
        }

        return TextSanitizer.Truncate(string.Join('\n', shown), 1024);
    }
}
