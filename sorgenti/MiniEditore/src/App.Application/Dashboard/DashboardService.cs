using App.Application.Abstractions;
using App.Domain.Documenti;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Dashboard;

public class DashboardService : IDashboardService
{
    private readonly IAppDbContext _db;
    private readonly IClock _clock;

    public DashboardService(IAppDbContext db, IClock clock)
    {
        _db = db;
        _clock = clock;
    }

    public async Task<DashboardSummaryDto> GetSummaryAsync(CancellationToken cancellationToken = default)
    {
        DateOnly today = _clock.Today;
        DateOnly firstDayOfMonth = new(today.Year, today.Month, 1);
        DateOnly lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

        int numeroLibriAttivi = await _db.Libri.CountAsync(x => x.Attivo, cancellationToken);
        int numeroAutori = await _db.Autori.CountAsync(cancellationToken);
        int numeroClienti = await _db.Clienti.CountAsync(x => x.Attivo, cancellationToken);

        IQueryable<App.Domain.Documenti.Documento> documentiMese = _db.Documenti
            .Where(x => x.Data >= firstDayOfMonth && x.Data <= lastDayOfMonth);

        int numeroDocumentiMese = await documentiMese.CountAsync(cancellationToken);

        // SQLite non supporta Sum server-side sui decimal.
        // Per il progetto didattico carichiamo solo gli importi necessari e sommiamo lato client.
        List<decimal> totaliDocumentiMese = await documentiMese
            .Where(x => x.Stato == StatoDocumento.Confermato)
            .Select(x => x.TotaleDocumento)
            .ToListAsync(cancellationToken);

        decimal totaleDocumentiMese = totaliDocumentiMese.Sum();

        List<UltimoDocumentoDto> ultimiDocumenti = await (
            from d in _db.Documenti
            join c in _db.Clienti on d.ClienteId equals c.Id
            orderby d.Data descending, d.Id descending
            select new UltimoDocumentoDto(
                d.Id,
                d.Numero,
                d.Data,
                c.RagioneSociale,
                d.TotaleDocumento))
            .Take(5)
            .ToListAsync(cancellationToken);

        var righeVendute = await (
            from r in _db.DocumentoRighe
            join d in _db.Documenti on r.DocumentoId equals d.Id
            join l in _db.Libri on r.LibroId equals l.Id
            where d.Stato == StatoDocumento.Confermato
            select new
            {
                r.LibroId,
                l.Titolo,
                r.Quantita,
                r.PrezzoUnitario,
                r.ScontoPercentuale
            })
            .ToListAsync(cancellationToken);

        List<LibroPiuVendutoDto> libriPiuVenduti = righeVendute
            .GroupBy(x => new { x.LibroId, x.Titolo })
            .Select(g => new LibroPiuVendutoDto(
                g.Key.LibroId,
                g.Key.Titolo,
                g.Sum(x => x.Quantita),
                g.Sum(x => x.Quantita * x.PrezzoUnitario * (1 - x.ScontoPercentuale / 100m))))
            .OrderByDescending(x => x.QuantitaVenduta)
            .Take(5)
            .ToList();

        return new DashboardSummaryDto(
            numeroLibriAttivi,
            numeroAutori,
            numeroClienti,
            numeroDocumentiMese,
            totaleDocumentiMese,
            ultimiDocumenti,
            libriPiuVenduti);
    }
}
