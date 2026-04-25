using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using App.Application.Common;

namespace App.Application.Catalogo.Collane;

public interface ICollanaService
{
    Task<IReadOnlyList<CollanaDto>> GetCollaneAsync(CancellationToken cancellationToken = default);
    Task<Result<CollanaDto>> GetCollanaAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<int>> CreateCollanaAsync(CreateCollanaRequest request, CancellationToken cancellationToken = default);
    Task<Result> UpdateCollanaAsync(int id, UpdateCollanaRequest request, CancellationToken cancellationToken = default);
}
