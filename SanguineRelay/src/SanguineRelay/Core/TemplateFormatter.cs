using System.Text.RegularExpressions;

namespace SanguineRelay.Core;

internal static class TemplateFormatter
{
    private static readonly Regex Placeholder = new("\\{([a-zA-Z][a-zA-Z0-9]*)\\}", RegexOptions.CultureInvariant | RegexOptions.Compiled);

    public static string Format(string template, IReadOnlyDictionary<string, string?> values)
    {
        if (string.IsNullOrEmpty(template))
        {
            return string.Empty;
        }

        return Placeholder.Replace(template, match =>
            values.TryGetValue(match.Groups[1].Value, out var value) ? value ?? string.Empty : match.Value);
    }
}
