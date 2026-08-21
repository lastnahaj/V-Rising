using System.Text.Json;
using BepInEx.Logging;

namespace SanguineRelay.Persistence;

internal sealed record PersistentState(ulong StatusMessageId = 0);

internal sealed class PersistenceStore
{
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };
    private readonly object _gate = new();
    private readonly string _path;
    private readonly ManualLogSource _log;
    private PersistentState _state;

    public PersistenceStore(string path, ManualLogSource log)
    {
        _path = path;
        _log = log;
        _state = Load();
    }

    public PersistentState Current
    {
        get
        {
            lock (_gate)
            {
                return _state;
            }
        }
    }

    public void SetStatusMessageId(ulong messageId) => Save(Current with { StatusMessageId = messageId });

    private PersistentState Load()
    {
        var temporaryPath = _path + ".tmp";
        foreach (var candidate in new[] { _path, temporaryPath })
        {
            if (!File.Exists(candidate))
            {
                continue;
            }

            try
            {
                var state = JsonSerializer.Deserialize<PersistentState>(File.ReadAllText(candidate), JsonOptions);
                if (state is null)
                {
                    continue;
                }

                if (candidate == temporaryPath)
                {
                    File.Move(temporaryPath, _path, true);
                    _log.LogWarning("Recovered persistent state from an interrupted atomic write.");
                }

                return state;
            }
            catch (Exception exception) when (exception is IOException or UnauthorizedAccessException or JsonException)
            {
                _log.LogWarning($"A persistent-state candidate could not be read ({exception.GetType().Name}).");
            }
        }

        return new PersistentState();
    }

    private void Save(PersistentState state)
    {
        lock (_gate)
        {
            var directory = Path.GetDirectoryName(_path) ?? throw new InvalidOperationException("Persistence path has no directory.");
            Directory.CreateDirectory(directory);
            var temporaryPath = _path + ".tmp";

            try
            {
                using (var stream = new FileStream(temporaryPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    JsonSerializer.Serialize(stream, state, JsonOptions);
                    stream.Flush(true);
                }

                File.Move(temporaryPath, _path, true);
                _state = state;
            }
            catch (Exception exception) when (exception is IOException or UnauthorizedAccessException)
            {
                _log.LogError($"Unable to save persistent state ({exception.GetType().Name}).");
            }
        }
    }
}
