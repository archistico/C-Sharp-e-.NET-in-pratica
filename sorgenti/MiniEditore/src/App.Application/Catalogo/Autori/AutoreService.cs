using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using App.Application.Abstractions;
using App.Application.Common;
using App.Domain.Catalogo;
using App.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Catalogo.Autori;

public class AutoreService : IAutoreService
{
    private readonly IAppDbContext _db;

    public AutoreService(IAppDbContext db) => _db = db;

    public async Task<IReadOnlyList<AutoreDto>> GetAutoriAsync(CancellationToken cancellationToken = default)
    {
        return await _db.Autori.Select(a => new AutoreDto(a.Id, a.Nome, a.Cognome, a.NomeVisualizzato, a.Email, a.Note))
            .ToListAsync(cancellationToken);
    }

    public async Task<Result<AutoreDto>> GetAutoreAsync(int id, CancellationToken cancellationToken = default)
    {
        var a = await _db.Autori.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (a == null) return Result<AutoreDto>.Failure("Autore non trovato");
        return Result<AutoreDto>.Success(new AutoreDto(a.Id, a.Nome, a.Cognome, a.NomeVisualizzato, a.Email, a.Note));
    }

    public async Task<Result<int>> CreateAutoreAsync(CreateAutoreRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            var a = new Autore(request.Nome, request.Cognome, request.Email, request.Note);
            await _db.Autori.AddAsync(a, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            return Result<int>.Success(a.Id);
        }
        catch (DomainException ex)
        {
            return Result<int>.Failure(ex.Message);
        }
    }

    public async Task<Result> UpdateAutoreAsync(int id, UpdateAutoreRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            var a = await _db.Autori.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (a == null) return Result.Failure("Autore non trovato");
            a.ImpostaDati(request.Nome, request.Cognome, request.Email, request.Note);
            await _db.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (DomainException ex)
        {
            return Result.Failure(ex.Message);
        }
    }
}
