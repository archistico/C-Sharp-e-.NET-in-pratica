using System.Text.Json;
using MiniInventario.Core.Features.Machines;

namespace MiniInventario.Core.Infrastructure.Storage;

public sealed class JsonMachineStorage
{
    private readonly string _filePath;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public JsonMachineStorage(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("Il percorso del file JSON non può essere vuoto.", nameof(filePath));
        }

        _filePath = filePath;
    }

    public IReadOnlyList<Machine> Load()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Machine>();
        }

        var json = File.ReadAllText(_filePath);

        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<Machine>();
        }

        var machines = JsonSerializer.Deserialize<List<Machine>>(json, JsonOptions);

        return machines ?? new List<Machine>();
    }

    public void Save(IEnumerable<Machine> machines)
    {
        ArgumentNullException.ThrowIfNull(machines);

        var directory = Path.GetDirectoryName(_filePath);

        if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var json = JsonSerializer.Serialize(machines, JsonOptions);

        File.WriteAllText(_filePath, json);
    }
}