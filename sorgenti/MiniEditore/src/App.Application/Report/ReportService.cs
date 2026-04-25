using App.Application.Abstractions;
using App.Domain.Documenti;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Report;

public class ReportService : IReportService
{
    private readonly IAppDbContext _db;

    public ReportService(IAppDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<VenditePerLibroDto>> GetVenditePerLibroAsync(ReportVenditeRequest request, CancellationToken cancellationToken = default)
    {
        ValidatePeriodo(request);

        // SQLite non supporta Sum server-side sui decimal.
        // Recuperiamo le righe filtrate e facciamo le aggregazioni in memoria.
        var righe = await (
            from r in _db.DocumentoRighe
            join d in _db.Documenti on r.DocumentoId equals d.Id
            join l in _db.Libri on r.LibroId equals l.Id
            where d.Stato == StatoDocumento.Confermato
                  && d.Data >= request.DataDa
                  && d.Data <= request.DataA
            select new
            {
                r.LibroId,
                l.Titolo,
                r.Quantita,
                r.PrezzoUnitario,
                r.ScontoPercentuale
            })
            .ToListAsync(cancellationToken);

        return righe
            .GroupBy(x => new { x.LibroId, x.Titolo })
            .Select(g => new VenditePerLibroDto(
                g.Key.LibroId,
                g.Key.Titolo,
                g.Sum(x => x.Quantita),
                g.Sum(x => x.Quantita * x.PrezzoUnitario * (1 - x.ScontoPercentuale / 100m))))
            .OrderByDescending(x => x.TotaleVenduto)
            .ToList();
    }

    public async Task<IReadOnlyList<VenditePerClienteDto>> GetVenditePerClienteAsync(ReportVenditeRequest request, CancellationToken cancellationToken = default)
    {
        ValidatePeriodo(request);

        var documenti = await (
            from d in _db.Documenti
            join c in _db.Clienti on d.ClienteId equals c.Id
            where d.Stato == StatoDocumento.Confermato
                  && d.Data >= request.DataDa
                  && d.Data <= request.DataA
            select new
            {
                d.ClienteId,
                Cliente = c.RagioneSociale,
                d.TotaleDocumento
            })
            .ToListAsync(cancellationToken);

        return documenti
            .GroupBy(x => new { x.ClienteId, x.Cliente })
            .Select(g => new VenditePerClienteDto(
                g.Key.ClienteId,
                g.Key.Cliente,
                g.Sum(x => x.TotaleDocumento)))
            .OrderByDescending(x => x.TotaleVenduto)
            .ToList();
    }

    public async Task<IReadOnlyList<RiepilogoMensileDto>> GetRiepilogoMensileAsync(ReportVenditeRequest request, CancellationToken cancellationToken = default)
    {
        ValidatePeriodo(request);

        var documenti = await _db.Documenti
            .Where(d => d.Stato == StatoDocumento.Confermato
                        && d.Data >= request.DataDa
                        && d.Data <= request.DataA)
            .Select(d => new
            {
                d.Data,
                d.TotaleDocumento
            })
            .ToListAsync(cancellationToken);

        return documenti
            .GroupBy(d => new { d.Data.Year, d.Data.Month })
            .Select(g => new RiepilogoMensileDto(
                g.Key.Year,
                g.Key.Month,
                g.Sum(x => x.TotaleDocumento),
                g.Count()))
            .OrderBy(x => x.Anno)
            .ThenBy(x => x.Mese)
            .ToList();
    }

    private static void ValidatePeriodo(ReportVenditeRequest request)
    {
        if (request.DataDa > request.DataA)
        {
            throw new ArgumentException("La data iniziale non può essere successiva alla data finale.");
        }
    }
}
