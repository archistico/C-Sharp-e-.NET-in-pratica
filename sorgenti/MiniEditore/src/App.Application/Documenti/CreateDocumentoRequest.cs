using System.Collections.Generic;

namespace App.Application.Documenti;

public sealed record CreateDocumentoRequest(DateOnly Data, int ClienteId, App.Domain.Documenti.TipoDocumento TipoDocumento, IReadOnlyList<CreateDocumentoRigaRequest> Righe);
