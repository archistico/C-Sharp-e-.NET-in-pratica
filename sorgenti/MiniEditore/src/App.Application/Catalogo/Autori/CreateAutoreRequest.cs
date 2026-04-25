namespace App.Application.Catalogo.Autori;

public sealed record CreateAutoreRequest(string Nome, string Cognome, string? Email, string? Note);
