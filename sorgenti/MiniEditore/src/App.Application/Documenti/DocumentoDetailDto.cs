using System.Collections.Generic;
using App.Domain.Documenti;

namespace App.Application.Documenti;

public sealed record DocumentoDetailDto(int Id, string Numero, DateOnly Data, int ClienteId, TipoDocumento TipoDocumento, StatoDocumento Stato, decimal TotaleImponibile, decimal TotaleSconto, decimal TotaleDocumento, IReadOnlyList<DocumentoRigaDto> Righe);
