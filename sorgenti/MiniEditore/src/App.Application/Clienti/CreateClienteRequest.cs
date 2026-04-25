namespace App.Application.Clienti;

public sealed record CreateClienteRequest(string RagioneSociale, string? Email, string? Telefono, string? PartitaIva, string? CodiceFiscale);
