using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using App.Application.Common;

namespace App.Application.Clienti;

public interface IClienteService
{
    Task<IReadOnlyList<ClienteDto>> GetClientiAsync(CancellationToken cancellationToken = default);
    Task<Result<ClienteDto>> GetClienteAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<int>> CreateClienteAsync(CreateClienteRequest request, CancellationToken cancellationToken = default);
    Task<Result> UpdateClienteAsync(int id, UpdateClienteRequest request, CancellationToken cancellationToken = default);
}
