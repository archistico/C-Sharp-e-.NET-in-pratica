namespace App.Application.Catalogo.Autori;

public sealed record UpdateAutoreRequest(string Nome, string Cognome, string? Email, string? Note);
