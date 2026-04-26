namespace MiniInventario.Core.Features.Machines;

public sealed class MachineService
{
    private readonly IMachineRepository _repository;
    private readonly MachineValidator _validator;

    public MachineService(IMachineRepository repository, MachineValidator validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public IReadOnlyList<Machine> GetAll()
    {
        return _repository
            .GetAll()
            .OrderBy(machine => machine.Name)
            .ToList();
    }

    public IReadOnlyList<Machine> Search(MachineSearchCriteria criteria)
    {
        ArgumentNullException.ThrowIfNull(criteria);

        var machines = _repository.GetAll();

        if (string.IsNullOrWhiteSpace(criteria.SearchText))
        {
            return machines
                .OrderBy(machine => machine.Name)
                .ToList();
        }

        var searchText = criteria.SearchText.Trim();

        return machines
            .Where(machine =>
                Contains(machine.Name, searchText) ||
                Contains(machine.AssignedUser, searchText) ||
                Contains(machine.Location, searchText))
            .OrderBy(machine => machine.Name)
            .ToList();
    }

    public Machine Create(CreateMachineRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var machines = _repository.GetAll().ToList();

        var validationResult = _validator.ValidateForCreate(request, machines);

        if (!validationResult.IsValid)
        {
            throw new InvalidOperationException(
                string.Join(Environment.NewLine, validationResult.Errors));
        }

        var now = DateTime.Now;

        var machine = new Machine
        {
            Id = Guid.NewGuid(),
            Name = request.Name.Trim(),
            AssignedUser = NormalizeOptionalText(request.AssignedUser),
            Location = request.Location.Trim(),
            Notes = NormalizeOptionalText(request.Notes),
            CreatedAt = now,
            UpdatedAt = now
        };

        machines.Add(machine);

        _repository.SaveAll(machines);

        return machine;
    }

    public void Update(UpdateMachineRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var machines = _repository.GetAll().ToList();

        var existingMachine = machines.FirstOrDefault(machine => machine.Id == request.Id);

        if (existingMachine is null)
        {
            throw new InvalidOperationException("La macchina da modificare non esiste.");
        }

        var validationResult = _validator.ValidateForUpdate(request, machines);

        if (!validationResult.IsValid)
        {
            throw new InvalidOperationException(
                string.Join(Environment.NewLine, validationResult.Errors));
        }

        existingMachine.Name = request.Name.Trim();
        existingMachine.AssignedUser = NormalizeOptionalText(request.AssignedUser);
        existingMachine.Location = request.Location.Trim();
        existingMachine.Notes = NormalizeOptionalText(request.Notes);
        existingMachine.UpdatedAt = DateTime.Now;

        _repository.SaveAll(machines);
    }

    public void Delete(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new InvalidOperationException("L'identificativo della macchina non è valido.");
        }

        var machines = _repository.GetAll().ToList();

        var machineToRemove = machines.FirstOrDefault(machine => machine.Id == id);

        if (machineToRemove is null)
        {
            throw new InvalidOperationException("La macchina da eliminare non esiste.");
        }

        machines.Remove(machineToRemove);

        _repository.SaveAll(machines);
    }

    private static bool Contains(string value, string searchText)
    {
        return value.Contains(searchText, StringComparison.OrdinalIgnoreCase);
    }

    private static string NormalizeOptionalText(string value)
    {
        return string.IsNullOrWhiteSpace(value)
            ? string.Empty
            : value.Trim();
    }
}