namespace App.Application.Clienti;

public sealed record UpdateClienteRequest(string RagioneSociale, string? Email, string? Telefono, string? PartitaIva, string? CodiceFiscale, bool Attivo);
