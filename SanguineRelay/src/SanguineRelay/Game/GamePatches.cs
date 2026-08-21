using HarmonyLib;
using ProjectM;
using Stunlock.Network;

namespace SanguineRelay.Game;

internal static class GamePatchContext
{
    public static GameIntegrationService? Integration { get; set; }
}

[HarmonyPatch(typeof(ServerBootstrapSystem), nameof(ServerBootstrapSystem.OnUpdate))]
internal static class ServerTickPatch
{
    [HarmonyPostfix]
    private static void Postfix(ServerBootstrapSystem __instance) => GamePatchContext.Integration?.Tick(__instance);
}

[HarmonyPatch(typeof(ServerBootstrapSystem), nameof(ServerBootstrapSystem.OnUserConnected))]
internal static class PlayerConnectedPatch
{
    [HarmonyPostfix]
    private static void Postfix(ServerBootstrapSystem __instance, NetConnectionId netConnectionId) =>
        GamePatchContext.Integration?.HandlePlayerConnected(__instance, netConnectionId);
}

[HarmonyPatch(typeof(ServerBootstrapSystem), nameof(ServerBootstrapSystem.OnUserDisconnected))]
internal static class PlayerDisconnectedPatch
{
    [HarmonyPrefix]
    private static void Prefix(
        ServerBootstrapSystem __instance,
        NetConnectionId netConnectionId,
        ConnectionStatusChangeReason connectionStatusReason) =>
        GamePatchContext.Integration?.HandlePlayerDisconnected(__instance, netConnectionId, connectionStatusReason);
}

[HarmonyPatch(typeof(ChatMessageSystem), nameof(ChatMessageSystem.OnUpdate))]
internal static class ChatMessagePatch
{
    [HarmonyPrefix]
    private static void Prefix(ChatMessageSystem __instance) => GamePatchContext.Integration?.HandleChat(__instance);
}

[HarmonyPatch(typeof(DeathEventListenerSystem), nameof(DeathEventListenerSystem.OnUpdate))]
internal static class DeathEventPatch
{
    [HarmonyPrefix]
    private static void Prefix(DeathEventListenerSystem __instance) => GamePatchContext.Integration?.HandleDeathEvents(__instance);
}

[HarmonyPatch(typeof(VampireDownedServerEventSystem), nameof(VampireDownedServerEventSystem.OnUpdate))]
internal static class PvpDownedPatch
{
    [HarmonyPostfix]
    private static void Postfix(VampireDownedServerEventSystem __instance) => GamePatchContext.Integration?.HandlePvpDowned(__instance);
}

[HarmonyPatch(typeof(VBloodSystem), nameof(VBloodSystem.OnUpdate))]
internal static class VBloodPatch
{
    [HarmonyPrefix]
    private static void Prefix(VBloodSystem __instance) => GamePatchContext.Integration?.HandleVBlood(__instance);
}
