using App.Domain.Documenti;

namespace App.Application.Documenti;

public sealed record DocumentoListItemDto(int Id, string Numero, DateOnly Data, string Cliente, StatoDocumento Stato, decimal TotaleDocumento);
