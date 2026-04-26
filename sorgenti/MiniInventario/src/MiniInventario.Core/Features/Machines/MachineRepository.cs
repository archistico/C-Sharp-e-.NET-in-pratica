using MiniInventario.Core.Infrastructure.Storage;

namespace MiniInventario.Core.Features.Machines;

public sealed class MachineRepository : IMachineRepository
{
    private readonly JsonMachineStorage _storage;

    public MachineRepository(JsonMachineStorage storage)
    {
        _storage = storage;
    }

    public IReadOnlyList<Machine> GetAll()
    {
        return _storage.Load();
    }

    public Machine? GetById(Guid id)
    {
        return _storage
            .Load()
            .FirstOrDefault(machine => machine.Id == id);
    }

    public void SaveAll(IEnumerable<Machine> machines)
    {
        _storage.Save(machines);
    }
}