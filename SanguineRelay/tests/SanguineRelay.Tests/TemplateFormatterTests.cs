using SanguineRelay.Core;

namespace SanguineRelay.Tests;

public sealed class TemplateFormatterTests
{
    [Fact]
    public void ReplacesKnownPlaceholdersAndPreservesUnknownOnes()
    {
        var values = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase)
        {
            ["online"] = "17",
            ["server"] = "Vardoran"
        };

        var result = TemplateFormatter.Format("{server}: {ONLINE} {future}", values);

        Assert.Equal("Vardoran: 17 {future}", result);
    }
}
