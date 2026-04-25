using Microsoft.EntityFrameworkCore;
using App.Application.Documenti;
using App.Domain.Documenti;
using App.Infrastructure.Data;

namespace App.Infrastructure.Documenti;

public class DocumentoRepository : IDocumentoRepository
{
    private readonly AppDbContext _db;

    public DocumentoRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Documento?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _db.Documenti
            .Include(d => d.Righe)
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
    }

    public async Task AddAsync(Documento documento, CancellationToken cancellationToken = default)
    {
        await _db.Documenti.AddAsync(documento, cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => _db.SaveChangesAsync(cancellationToken);
}
