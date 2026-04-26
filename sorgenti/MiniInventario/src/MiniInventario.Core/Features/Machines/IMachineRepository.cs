namespace MiniInventario.Core.Features.Machines;

public interface IMachineRepository
{
    IReadOnlyList<Machine> GetAll();

    Machine? GetById(Guid id);

    void SaveAll(IEnumerable<Machine> machines);
}