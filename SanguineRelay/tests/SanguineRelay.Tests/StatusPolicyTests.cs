using SanguineRelay.Discord;

namespace SanguineRelay.Tests;

public sealed class StatusPolicyTests
{
    [Fact]
    public void UnchangedTimestampedEmbedRefreshesOnlyOnIntervalOrForcedPublication()
    {
        Assert.False(StatusRefreshPolicy.ShouldModifyExistingEmbed(false, true, StatusUpdateReason.StateChanged));
        Assert.True(StatusRefreshPolicy.ShouldModifyExistingEmbed(false, true, StatusUpdateReason.Interval));
        Assert.True(StatusRefreshPolicy.ShouldModifyExistingEmbed(false, true, StatusUpdateReason.Shutdown));
        Assert.False(StatusRefreshPolicy.ShouldModifyExistingEmbed(false, false, StatusUpdateReason.Interval));
        Assert.True(StatusRefreshPolicy.ShouldModifyExistingEmbed(true, false, StatusUpdateReason.StateChanged));
    }

    [Fact]
    public void FinalOfflinePlanSchedulesOnlyEnabledStatusFeatures()
    {
        var plan = StatusRefreshPolicy.CreateFinalOfflinePlan(true, true, false);

        Assert.True(plan.Presence);
        Assert.True(plan.StatusEmbed);
        Assert.False(plan.VoiceCounter);
    }
}
