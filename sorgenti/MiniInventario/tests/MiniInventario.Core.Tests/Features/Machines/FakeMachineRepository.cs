using MiniInventario.Core.Features.Machines;

namespace MiniInventario.Core.Tests.Features.Machines;

public sealed class FakeMachineRepository : IMachineRepository
{
    private List<Machine> _machines = new();

    public FakeMachineRepository()
    {
    }

    public FakeMachineRepository(IEnumerable<Machine> machines)
    {
        _machines = machines.ToList();
    }

    public IReadOnlyList<Machine> GetAll()
    {
        return _machines.ToList();
    }

    public Machine? GetById(Guid id)
    {
        return _machines.FirstOrDefault(machine => machine.Id == id);
    }

    public void SaveAll(IEnumerable<Machine> machines)
    {
        _machines = machines.ToList();
    }
}