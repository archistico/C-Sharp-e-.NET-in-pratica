using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using App.Application.Common;

namespace App.Application.Catalogo.Autori;

public interface IAutoreService
{
    Task<IReadOnlyList<AutoreDto>> GetAutoriAsync(CancellationToken cancellationToken = default);
    Task<Result<AutoreDto>> GetAutoreAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<int>> CreateAutoreAsync(CreateAutoreRequest request, CancellationToken cancellationToken = default);
    Task<Result> UpdateAutoreAsync(int id, UpdateAutoreRequest request, CancellationToken cancellationToken = default);
}
