namespace App.Application.Catalogo.Autori;

public sealed record AutoreDto(int Id, string Nome, string Cognome, string NomeVisualizzato, string? Email, string? Note);
