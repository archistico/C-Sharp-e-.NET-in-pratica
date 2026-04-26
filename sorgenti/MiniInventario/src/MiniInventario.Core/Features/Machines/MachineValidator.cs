namespace MiniInventario.Core.Features.Machines;

public sealed class MachineValidator
{
    public MachineValidationResult ValidateForCreate(
        CreateMachineRequest request,
        IEnumerable<Machine> existingMachines)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(existingMachines);

        var result = new MachineValidationResult();

        ValidateName(request.Name, result);
        ValidateLocation(request.Location, result);
        ValidateDuplicateName(request.Name, existingMachines, null, result);

        return result;
    }

    public MachineValidationResult ValidateForUpdate(
        UpdateMachineRequest request,
        IEnumerable<Machine> existingMachines)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(existingMachines);

        var result = new MachineValidationResult();

        if (request.Id == Guid.Empty)
        {
            result.AddError("L'identificativo della macchina non è valido.");
        }

        ValidateName(request.Name, result);
        ValidateLocation(request.Location, result);
        ValidateDuplicateName(request.Name, existingMachines, request.Id, result);

        return result;
    }

    private static void ValidateName(string name, MachineValidationResult result)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            result.AddError("Il nome macchina è obbligatorio.");
        }
    }

    private static void ValidateLocation(string location, MachineValidationResult result)
    {
        if (string.IsNullOrWhiteSpace(location))
        {
            result.AddError("La posizione è obbligatoria.");
        }
    }

    private static void ValidateDuplicateName(
        string name,
        IEnumerable<Machine> existingMachines,
        Guid? currentMachineId,
        MachineValidationResult result)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return;
        }

        var normalizedName = name.Trim();

        var duplicateExists = existingMachines.Any(machine =>
            machine.Name.Equals(normalizedName, StringComparison.OrdinalIgnoreCase)
            && (!currentMachineId.HasValue || machine.Id != currentMachineId.Value));

        if (duplicateExists)
        {
            result.AddError("Esiste già una macchina con questo nome.");
        }
    }
}