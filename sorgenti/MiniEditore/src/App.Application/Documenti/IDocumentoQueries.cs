using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Documenti;

public interface IDocumentoQueries
{
    Task<IReadOnlyList<DocumentoListItemDto>> GetDocumentiAsync(CancellationToken cancellationToken = default);
    Task<DocumentoDetailDto?> GetDocumentoDetailAsync(int id, CancellationToken cancellationToken = default);
}
