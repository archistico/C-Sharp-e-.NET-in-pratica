namespace MiniInventario.Core.Features.Machines;

public sealed class CreateMachineRequest
{
    public string Name { get; set; } = string.Empty;

    public string AssignedUser { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;
}