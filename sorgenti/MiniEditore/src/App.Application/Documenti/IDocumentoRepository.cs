using System.Threading;
using System.Threading.Tasks;
using App.Domain.Documenti;

namespace App.Application.Documenti;

public interface IDocumentoRepository
{
    Task<Documento?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(Documento documento, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
