using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using App.Application.Abstractions;
using App.Application.Common;
using App.Domain.Clienti;
using App.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Clienti;

public class ClienteService : IClienteService
{
    private readonly IAppDbContext _db;

    public ClienteService(IAppDbContext db) => _db = db;

    public async Task<IReadOnlyList<ClienteDto>> GetClientiAsync(CancellationToken cancellationToken = default)
    {
        return await _db.Clienti.Select(c => new ClienteDto(c.Id, c.RagioneSociale, c.Email, c.Telefono, c.PartitaIva, c.CodiceFiscale, c.Attivo)).ToListAsync(cancellationToken);
    }

    public async Task<Result<ClienteDto>> GetClienteAsync(int id, CancellationToken cancellationToken = default)
    {
        var c = await _db.Clienti.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (c == null) return Result<ClienteDto>.Failure("Cliente non trovato");
        return Result<ClienteDto>.Success(new ClienteDto(c.Id, c.RagioneSociale, c.Email, c.Telefono, c.PartitaIva, c.CodiceFiscale, c.Attivo));
    }

    public async Task<Result<int>> CreateClienteAsync(CreateClienteRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            var c = new Cliente(request.RagioneSociale, request.Email, request.Telefono, request.PartitaIva, request.CodiceFiscale);
            await _db.Clienti.AddAsync(c, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            return Result<int>.Success(c.Id);
        }
        catch (DomainException ex)
        {
            return Result<int>.Failure(ex.Message);
        }
    }

    public async Task<Result> UpdateClienteAsync(int id, UpdateClienteRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            var c = await _db.Clienti.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (c == null) return Result.Failure("Cliente non trovato");
            c.ImpostaDati(request.RagioneSociale, request.Email, request.Telefono, request.PartitaIva, request.CodiceFiscale);
            if (request.Attivo) c.AttivaCliente(); else c.DisattivaCliente();
            await _db.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (DomainException ex)
        {
            return Result.Failure(ex.Message);
        }
    }
}
