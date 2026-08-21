using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace SanguineRelay.Core;

internal static class TextSanitizer
{
    private const int DiscordMessageLimit = 2000;
    private const int GameMessageLimit = 500;
    private static readonly Regex DiscordMentionPattern = new("<(@[!&]?|#)(\\d{1,20})>", RegexOptions.CultureInvariant | RegexOptions.Compiled);
    private static readonly Regex DiscordMarkdownPattern = new("[`*_~|>]", RegexOptions.CultureInvariant | RegexOptions.Compiled);
    private static readonly Regex DiscordDisplayMarkdownPattern = new("[\\\\`*_~|>]", RegexOptions.CultureInvariant | RegexOptions.Compiled);
    private static readonly Regex DiscordChatMarkdownPattern = new("[\\\\`*_~|]", RegexOptions.CultureInvariant | RegexOptions.Compiled);

    public static string DiscordDisplay(string value, int maximumLength)
    {
        var clean = Normalize(value, allowNewLines: false);
        clean = NeutralizeDiscordMentions(clean);
        clean = DiscordDisplayMarkdownPattern.Replace(clean, match => $"\\{match.Value}");
        return Truncate(clean, Math.Min(maximumLength, DiscordMessageLimit));
    }

    public static string DiscordChatContent(string value, bool allowMentions, int maximumLength)
    {
        var clean = Normalize(value, allowNewLines: false);
        if (!allowMentions)
        {
            clean = NeutralizeDiscordMentions(clean);
        }

        clean = DiscordChatMarkdownPattern.Replace(clean, match => $"\\{match.Value}");
        return Truncate(clean, Math.Min(maximumLength, DiscordMessageLimit));
    }

    public static string DiscordPlainText(string value, int maximumLength)
    {
        var clean = NeutralizeDiscordMentions(Normalize(value, allowNewLines: false));
        return Truncate(clean, Math.Min(maximumLength, DiscordMessageLimit));
    }

    public static string FinalizeDiscordMessage(string value, bool allowMentions = false, int maximumLength = DiscordMessageLimit)
    {
        var clean = Normalize(value, allowNewLines: true);
        if (!allowMentions)
        {
            clean = NeutralizeDiscordMentions(clean);
        }

        return Truncate(clean, Math.Min(maximumLength, DiscordMessageLimit));
    }

    public static string ForGame(string value, int maximumLength)
    {
        var clean = Normalize(value, allowNewLines: false);
        clean = DiscordMarkdownPattern.Replace(clean, string.Empty);
        clean = DiscordMentionPattern.Replace(clean, match => $"@{match.Groups[2].Value}");
        clean = clean
            .Replace("@everyone", "everyone", StringComparison.OrdinalIgnoreCase)
            .Replace("@here", "here", StringComparison.OrdinalIgnoreCase);
        if (clean.StartsWith("/", StringComparison.Ordinal))
        {
            clean = "／" + clean[1..];
        }

        return Truncate(clean, Math.Min(maximumLength, GameMessageLimit));
    }

    public static string GameDisplayName(string value) => Truncate(ForGame(value, 80), 80);

    public static string Truncate(string value, int maximumLength)
    {
        if (maximumLength <= 0 || value.Length == 0)
        {
            return string.Empty;
        }

        if (value.Length <= maximumLength)
        {
            return value;
        }

        if (maximumLength == 1)
        {
            return "…";
        }

        var contentLength = maximumLength - 1;
        if (contentLength < value.Length && contentLength > 0 &&
            char.IsHighSurrogate(value[contentLength - 1]) && char.IsLowSurrogate(value[contentLength]))
        {
            contentLength--;
        }

        return value[..contentLength] + "…";
    }

    private static string Normalize(string value, bool allowNewLines)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        var normalized = value.Normalize(NormalizationForm.FormKC);
        var builder = new StringBuilder(normalized.Length);
        var previousWhitespace = false;

        foreach (var rune in normalized.EnumerateRunes())
        {
            var category = Rune.GetUnicodeCategory(rune);
            if (category is UnicodeCategory.Control or UnicodeCategory.Format or UnicodeCategory.Surrogate)
            {
                if (category == UnicodeCategory.Format && !previousWhitespace)
                {
                    builder.Append(' ');
                    previousWhitespace = true;
                }
                else if (allowNewLines && rune.Value == '\n')
                {
                    builder.Append('\n');
                    previousWhitespace = false;
                }
                else if (rune.Value is '\r' or '\n' or '\t' && !previousWhitespace)
                {
                    builder.Append(' ');
                    previousWhitespace = true;
                }

                continue;
            }

            if (Rune.IsWhiteSpace(rune))
            {
                if (!previousWhitespace)
                {
                    builder.Append(' ');
                    previousWhitespace = true;
                }

                continue;
            }

            builder.Append(rune.ToString());
            previousWhitespace = false;
        }

        return builder.ToString().Trim();
    }

    private static string NeutralizeDiscordMentions(string value)
    {
        var clean = value
            .Replace("@everyone", "＠everyone", StringComparison.OrdinalIgnoreCase)
            .Replace("@here", "＠here", StringComparison.OrdinalIgnoreCase);
        return DiscordMentionPattern.Replace(clean, match => $"＜{match.Value[1..]}");
    }
}
