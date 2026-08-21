using System.Text.Json;
using BepInEx.Logging;
using SanguineRelay.Persistence;

namespace SanguineRelay.Tests;

public sealed class PersistenceStoreTests
{
    [Fact]
    public void AtomicReplacementPersistsLatestStateWithoutTemporaryFile()
    {
        var directory = CreateTemporaryDirectory();
        try
        {
            var path = Path.Combine(directory, "state.json");
            var store = new PersistenceStore(path, new ManualLogSource("persistence-tests"));

            store.SetStatusMessageId(10);
            store.SetStatusMessageId(20);

            var persisted = JsonSerializer.Deserialize<PersistentState>(File.ReadAllText(path));
            Assert.Equal(20UL, persisted?.StatusMessageId);
            Assert.False(File.Exists(path + ".tmp"));
        }
        finally
        {
            Directory.Delete(directory, true);
        }
    }

    [Fact]
    public void RecoversValidTemporaryStateAfterInterruptedWrite()
    {
        var directory = CreateTemporaryDirectory();
        try
        {
            var path = Path.Combine(directory, "state.json");
            File.WriteAllText(path + ".tmp", "{\"StatusMessageId\":42}");

            var store = new PersistenceStore(path, new ManualLogSource("recovery-tests"));

            Assert.Equal(42UL, store.Current.StatusMessageId);
            Assert.True(File.Exists(path));
            Assert.False(File.Exists(path + ".tmp"));
        }
        finally
        {
            Directory.Delete(directory, true);
        }
    }

    [Fact]
    public void CorruptStateFallsBackWithoutThrowing()
    {
        var directory = CreateTemporaryDirectory();
        try
        {
            var path = Path.Combine(directory, "state.json");
            File.WriteAllText(path, "not-json");

            var store = new PersistenceStore(path, new ManualLogSource("corrupt-tests"));

            Assert.Equal(0UL, store.Current.StatusMessageId);
        }
        finally
        {
            Directory.Delete(directory, true);
        }
    }

    private static string CreateTemporaryDirectory()
    {
        var path = Path.Combine(Path.GetTempPath(), $"SanguineRelay-tests-{Guid.NewGuid():N}");
        Directory.CreateDirectory(path);
        return path;
    }
}
