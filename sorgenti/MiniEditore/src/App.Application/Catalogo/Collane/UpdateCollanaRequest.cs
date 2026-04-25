namespace App.Application.Catalogo.Collane;

public sealed record UpdateCollanaRequest(string Nome, string? Descrizione, bool Attiva);
