using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using SanguineRelay.Discord;
using SanguineRelay.Game;
using SanguineRelay.Persistence;
using SanguineRelay.State;

namespace SanguineRelay.Core;

[BepInPlugin(PluginInfo.Guid, PluginInfo.Name, PluginInfo.Version)]
public sealed class SanguineRelayPlugin : BasePlugin
{
    private Harmony? _harmony;
    private DiscordService? _discord;
    private GameThreadDispatcher? _dispatcher;

    public override void Load()
    {
        Log.LogInfo($"Starting {PluginInfo.Name} v{PluginInfo.Version}");

        var options = new ConfigurationService(Config, Log).Load();
        if (!options.General.Enabled)
        {
            Log.LogWarning("SanguineRelay is disabled by configuration.");
            return;
        }

        _dispatcher = new GameThreadDispatcher();
        var state = new ServerStateService(
            options.ServerInfo.DisplayName,
            options.ServerInfo.IpPortDisplayOverride,
            options.ServerInfo.ResetStartDateUtc);
        var persistencePath = Path.Combine(Paths.ConfigPath, PluginInfo.Name, "state.json");
        var persistence = new PersistenceStore(persistencePath, Log);
        var game = new GameIntegrationService(options, state, _dispatcher, Log);

        _discord = new DiscordService(options, state, game, _dispatcher, persistence, Log);
        GamePatchContext.Integration = game;
        _harmony = new Harmony(PluginInfo.Guid);
        _harmony.PatchAll(typeof(SanguineRelayPlugin).Assembly);

        _ = Task.Run(_discord.StartAsync);
        Log.LogInfo("SanguineRelay loaded. Waiting for the V Rising server world.");
    }

    public override bool Unload()
    {
        _dispatcher?.Shutdown();
        GamePatchContext.Integration = null;
        _harmony?.UnpatchSelf();

        if (_discord is not null)
        {
            try
            {
                var shutdown = Task.Run(async () =>
                {
                    await _discord.NotifyStoppingAsync().ConfigureAwait(false);
                    await _discord.DisposeAsync().ConfigureAwait(false);
                });
                if (!shutdown.Wait(TimeSpan.FromSeconds(15)))
                {
                    Log.LogWarning("Discord shutdown exceeded 15 seconds; server shutdown will continue.");
                }
            }
            catch (Exception exception)
            {
                Log.LogWarning($"Shutdown reported {exception.GetType().Name}: {exception.Message}");
            }
        }

        _dispatcher?.Dispose();

        Log.LogInfo("SanguineRelay stopped.");
        return true;
    }
}
