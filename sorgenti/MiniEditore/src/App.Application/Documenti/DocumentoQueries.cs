using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using App.Application.Abstractions;

namespace App.Application.Documenti;

internal class DocumentoQueries : IDocumentoQueries
{
    private readonly IAppDbContext _db;

    public DocumentoQueries(IAppDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<DocumentoListItemDto>> GetDocumentiAsync(CancellationToken cancellationToken = default)
    {
        var q = from d in _db.Documenti
                join c in _db.Clienti on d.ClienteId equals c.Id
                orderby d.Data descending, d.Id descending
                select new DocumentoListItemDto(d.Id, d.Numero, d.Data, c.RagioneSociale, d.Stato, d.TotaleDocumento);

        return await q.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<DocumentoDetailDto?> GetDocumentoDetailAsync(int id, CancellationToken cancellationToken = default)
    {
        var d = await _db.Documenti
            .AsNoTracking()
            .Include(x => x.Righe)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (d == null) return null;

        var dto = new DocumentoDetailDto(
            d.Id,
            d.Numero,
            d.Data,
            d.ClienteId,
            d.TipoDocumento,
            d.Stato,
            d.TotaleImponibile,
            d.TotaleSconto,
            d.TotaleDocumento,
            d.Righe.Select(r => new DocumentoRigaDto(r.Id, r.LibroId, r.Descrizione, r.Quantita, r.PrezzoUnitario, r.ScontoPercentuale, r.TotaleRiga)).ToList()
        );

        return dto;
    }
}
