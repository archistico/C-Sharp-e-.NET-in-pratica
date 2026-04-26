namespace MiniInventario.Core.Features.Machines;

public sealed class Machine
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string AssignedUser { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}