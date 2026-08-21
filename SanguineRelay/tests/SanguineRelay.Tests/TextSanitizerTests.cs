using SanguineRelay.Core;

namespace SanguineRelay.Tests;

public sealed class TextSanitizerTests
{
    [Fact]
    public void DiscordOutputNeutralizesMassAndStructuredMentions()
    {
        var result = TextSanitizer.DiscordChatContent("@everyone <@123> <@&456> <#789>", false, 2000);

        Assert.DoesNotContain("@everyone", result, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("<@123>", result, StringComparison.Ordinal);
        Assert.DoesNotContain("<@&456>", result, StringComparison.Ordinal);
        Assert.DoesNotContain("<#789>", result, StringComparison.Ordinal);
    }

    [Fact]
    public void GameOutputCannotBecomeSlashCommandAndRemovesControlText()
    {
        var result = TextSanitizer.ForGame("/admin\n@here\u200B", 300);

        Assert.StartsWith("／", result, StringComparison.Ordinal);
        Assert.DoesNotContain('\n', result);
        Assert.DoesNotContain("@here", result, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain('\u200B', result);
    }

    [Fact]
    public void TruncationNeverExceedsRequestedLimit()
    {
        var result = TextSanitizer.Truncate(new string('x', 500), 100);

        Assert.Equal(100, result.Length);
        Assert.EndsWith("…", result, StringComparison.Ordinal);
    }

    [Fact]
    public void TruncationDoesNotSplitUnicodeSurrogatePairs()
    {
        var result = TextSanitizer.Truncate("abc😀def", 5);

        Assert.Equal("abc…", result);
        Assert.DoesNotContain('\uFFFD', result);
    }

    [Fact]
    public void DiscordDisplayEscapesMarkdownWithoutDoubleEscapingDuringFinalization()
    {
        var display = TextSanitizer.DiscordDisplay("**name** `code` \\ path", 200);
        var finalized = TextSanitizer.FinalizeDiscordMessage($"**{display}:** fixed");

        Assert.Equal(@"**\*\*name\*\* \`code\` \\ path:** fixed", finalized);
    }

    [Fact]
    public void DiscordDisplayRemovesNewlinesBidiAndZeroWidthCharacters()
    {
        var result = TextSanitizer.DiscordDisplay("left\nright\u202Ehidden\u200B", 200);

        Assert.Equal("left right hidden", result);
    }

    [Fact]
    public void ExplicitGameChatMentionOptInPreservesMentionsOnlyForThatContent()
    {
        var allowed = TextSanitizer.DiscordChatContent("@everyone <@123>", true, 2000);
        var blocked = TextSanitizer.DiscordChatContent("@everyone <@123>", false, 2000);

        Assert.Contains("@everyone", allowed, StringComparison.Ordinal);
        Assert.Contains("<@123>", allowed, StringComparison.Ordinal);
        Assert.DoesNotContain("@everyone", blocked, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("<@123>", blocked, StringComparison.Ordinal);
    }

    [Fact]
    public void DiscordDisplayHandlesEmojiAtMaximumLength()
    {
        var result = TextSanitizer.DiscordDisplay("123😀567", 5);

        Assert.Equal("123…", result);
        Assert.DoesNotContain('\uFFFD', result);
    }
}
