using Discord;
using Discord.Net;
using SanguineRelay.Core;
using SanguineRelay.Discord;

namespace SanguineRelay.Tests;

public sealed class DiscordPolicyTests
{
    private static readonly IReadOnlySet<ulong> IgnoredUsers = new HashSet<ulong> { 10 };
    private static readonly IReadOnlySet<ulong> IgnoredBots = new HashSet<ulong> { 20 };

    [Theory]
    [InlineData(true, false, false, false, false, true)]
    [InlineData(false, true, false, false, false, true)]
    [InlineData(false, false, true, false, false, true)]
    [InlineData(false, false, false, true, false, true)]
    [InlineData(false, false, false, false, true, false)]
    public void BotFilteringHonorsExplicitPolicy(
        bool ownBot,
        bool webhook,
        bool system,
        bool ignoreAllBots,
        bool allowThirdPartyBot,
        bool expectedIgnored)
    {
        var context = new DiscordInboundMessageContext(30, true, ownBot, webhook, system);
        var ignored = DiscordInboundPolicy.ShouldIgnore(
            context,
            ignoreAllBots && !allowThirdPartyBot,
            IgnoredUsers,
            IgnoredBots);

        Assert.Equal(expectedIgnored, ignored);
    }

    [Fact]
    public void ExplicitIgnoredIdsAlwaysWin()
    {
        Assert.True(DiscordInboundPolicy.ShouldIgnore(
            new DiscordInboundMessageContext(10, false, false, false, false),
            false,
            IgnoredUsers,
            IgnoredBots));
        Assert.True(DiscordInboundPolicy.ShouldIgnore(
            new DiscordInboundMessageContext(20, true, false, false, false),
            false,
            IgnoredUsers,
            IgnoredBots));
    }

    [Fact]
    public void RetryPolicyUsesIdempotencyAndBoundedDiscordTiming()
    {
        Assert.False(DiscordRetryPolicy.ShouldRetry(new TimeoutException(), false));
        Assert.True(DiscordRetryPolicy.ShouldRetry(new TimeoutException(), true));
        Assert.True(DiscordRetryPolicy.ShouldRetry(new RateLimitedException(null!), false));
        Assert.Equal(TimeSpan.FromSeconds(12), DiscordRetryPolicy.GetDelay(
            new RateLimitedException(null!),
            1,
            TimeSpan.FromSeconds(12)));
        Assert.Equal(TimeSpan.FromSeconds(30), DiscordRetryPolicy.GetDelay(
            new RateLimitedException(null!),
            1,
            TimeSpan.FromMinutes(5)));
    }

    [Fact]
    public void AuditPolicyCoversEveryPrivilegedCommandAndOutcome()
    {
        Assert.True(DiscordAuditPolicy.RequiresAudit("player"));
        Assert.True(DiscordAuditPolicy.RequiresAudit("announce"));
        Assert.True(DiscordAuditPolicy.RequiresAudit("relay-status"));
        Assert.False(DiscordAuditPolicy.RequiresAudit("status"));
        Assert.Equal("Denied", DiscordAuditPolicy.Describe(AuditOutcome.Denied));
        Assert.Equal("Validation failure (missing value)", DiscordAuditPolicy.Describe(AuditOutcome.ValidationFailure, "missing value"));
    }

    [Fact]
    public void PermissionResolverHonorsGuildAndRoleInheritance()
    {
        var permissions = new DiscordPermissionService(
            1,
            new PermissionOptions(new HashSet<ulong> { 100 }, new HashSet<ulong> { 200 }));

        Assert.True(permissions.CanExecute(1, new[] { 100UL }, RelayPermission.Administer));
        Assert.True(permissions.CanExecute(1, new[] { 100UL }, RelayPermission.Announce));
        Assert.True(permissions.CanExecute(1, new[] { 200UL }, RelayPermission.ViewPlayer));
        Assert.False(permissions.CanExecute(1, new[] { 200UL }, RelayPermission.Administer));
        Assert.False(permissions.CanExecute(2, new[] { 100UL }, RelayPermission.Administer));
    }

    [Fact]
    public void VoiceLockPreservesUnrelatedOverwriteValues()
    {
        var original = OverwritePermissions.InheritAll.Modify(
            sendMessages: PermValue.Deny,
            manageChannel: PermValue.Allow);

        var locked = VoicePermissionPolicy.ApplyLock(original);

        Assert.Equal(PermValue.Deny, locked.SendMessages);
        Assert.Equal(PermValue.Allow, locked.ManageChannel);
        Assert.Equal(PermValue.Allow, locked.ViewChannel);
        Assert.Equal(PermValue.Deny, locked.Connect);
        Assert.Equal(PermValue.Deny, locked.Speak);
    }
}
