namespace App.Application.Clienti;

public sealed record ClienteDto(int Id, string RagioneSociale, string? Email, string? Telefono, string? PartitaIva, string? CodiceFiscale, bool Attivo);
