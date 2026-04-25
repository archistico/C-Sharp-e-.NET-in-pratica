using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using App.Application.Common;

namespace App.Application.Catalogo.Libri;

public interface ILibroService
{
    Task<IReadOnlyList<LibroListItemDto>> GetLibriAsync(CancellationToken cancellationToken = default);
    Task<Result<LibroDetailDto>> GetLibroAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<int>> CreateLibroAsync(CreateLibroRequest request, CancellationToken cancellationToken = default);
    Task<Result> UpdateLibroAsync(int id, UpdateLibroRequest request, CancellationToken cancellationToken = default);
    Task<Result> DeleteLibroAsync(int id, CancellationToken cancellationToken = default);
}
