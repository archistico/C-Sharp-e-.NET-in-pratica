namespace App.Domain.Common;

public abstract class Entity
{
    public int Id { get; protected set; }

    // Parameterless ctor for EF / serialization
    protected Entity() { }
}
