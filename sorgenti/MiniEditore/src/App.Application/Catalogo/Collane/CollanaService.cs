using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using App.Application.Abstractions;
using App.Application.Common;
using App.Domain.Catalogo;
using App.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Catalogo.Collane;

public class CollanaService : ICollanaService
{
    private readonly IAppDbContext _db;

    public CollanaService(IAppDbContext db) => _db = db;

    public async Task<IReadOnlyList<CollanaDto>> GetCollaneAsync(CancellationToken cancellationToken = default)
    {
        return await _db.Collane
            .Select(c => new CollanaDto(c.Id, c.Nome, c.Descrizione, c.Attiva))
            .ToListAsync(cancellationToken);
    }

    public async Task<Result<CollanaDto>> GetCollanaAsync(int id, CancellationToken cancellationToken = default)
    {
        var c = await _db.Collane.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (c == null) return Result<CollanaDto>.Failure("Collana non trovata");
        return Result<CollanaDto>.Success(new CollanaDto(c.Id, c.Nome, c.Descrizione, c.Attiva));
    }

    public async Task<Result<int>> CreateCollanaAsync(CreateCollanaRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            var c = new Collana(request.Nome, request.Descrizione);
            await _db.Collane.AddAsync(c, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            return Result<int>.Success(c.Id);
        }
        catch (DomainException ex)
        {
            return Result<int>.Failure(ex.Message);
        }
    }

    public async Task<Result> UpdateCollanaAsync(int id, UpdateCollanaRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            var c = await _db.Collane.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (c == null) return Result.Failure("Collana non trovata");
            c.ImpostaDati(request.Nome, request.Descrizione);
            if (request.Attiva) c.AttivaCollana(); else c.DisattivaCollana();
            await _db.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (DomainException ex)
        {
            return Result.Failure(ex.Message);
        }
    }
}
