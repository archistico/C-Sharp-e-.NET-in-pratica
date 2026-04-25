using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using App.Application.Abstractions;
using App.Application.Common;
using App.Domain.Catalogo;
using App.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Catalogo.Libri;

public class LibroService : ILibroService
{
    private readonly IAppDbContext _db;

    public LibroService(IAppDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<LibroListItemDto>> GetLibriAsync(CancellationToken cancellationToken = default)
    {
        var items = await _db.Libri
            .Select(l => new LibroListItemDto(l.Id, l.Titolo, l.Isbn, l.Prezzo, l.Attivo))
            .ToListAsync(cancellationToken);

        return items;
    }

    public async Task<Result<LibroDetailDto>> GetLibroAsync(int id, CancellationToken cancellationToken = default)
    {
        var l = await _db.Libri
            .Include(x => x.Autori)
            .ThenInclude(la => la.Autore)
            .Include(x => x.Collana)
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (l == null) return Result<LibroDetailDto>.Failure("Libro non trovato");

        var autori = l.Autori.Select(a => a.Autore?.NomeVisualizzato ?? string.Empty).ToList();
        var dto = new LibroDetailDto(l.Id, l.Titolo, l.Isbn, l.Prezzo, l.DataPubblicazione, l.Collana?.Nome, autori, l.Attivo);
        return Result<LibroDetailDto>.Success(dto);
    }

    public async Task<Result<int>> CreateLibroAsync(CreateLibroRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(request.Isbn))
            {
                var exists = await _db.Libri.AnyAsync(x => x.Isbn == request.Isbn, cancellationToken);
                if (exists) return Result<int>.Failure("ISBN già presente");
            }

            if (request.CollanaId.HasValue)
            {
                var coll = await _db.Collane.FindAsync(new object[] { request.CollanaId.Value }, cancellationToken);
                if (coll == null) return Result<int>.Failure("Collana non trovata");
            }

            if (request.AutoriIds != null && request.AutoriIds.Any())
            {
                var all = await _db.Autori.Where(a => request.AutoriIds.Contains(a.Id)).Select(a => a.Id).ToListAsync(cancellationToken);
                if (all.Count != request.AutoriIds.Count) return Result<int>.Failure("Alcuni autori non esistono");
            }

            var libro = new Libro(request.Titolo, request.Isbn, request.Prezzo, request.DataPubblicazione, request.CollanaId);
            if (request.AutoriIds != null) libro.ImpostaAutori(request.AutoriIds);

            await _db.Libri.AddAsync(libro, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);

            return Result<int>.Success(libro.Id);
        }
        catch (DomainException ex)
        {
            return Result<int>.Failure(ex.Message);
        }
    }

    public async Task<Result> UpdateLibroAsync(int id, UpdateLibroRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            var libro = await _db.Libri.Include(x => x.Autori).SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (libro == null) return Result.Failure("Libro non trovato");

            if (!string.IsNullOrWhiteSpace(request.Isbn))
            {
                var other = await _db.Libri.SingleOrDefaultAsync(x => x.Isbn == request.Isbn && x.Id != id, cancellationToken);
                if (other != null) return Result.Failure("ISBN già presente su altro libro");
            }

            if (request.CollanaId.HasValue)
            {
                var coll = await _db.Collane.FindAsync(new object[] { request.CollanaId.Value }, cancellationToken);
                if (coll == null) return Result.Failure("Collana non trovata");
            }

            if (request.AutoriIds != null && request.AutoriIds.Any())
            {
                var all = await _db.Autori.Where(a => request.AutoriIds.Contains(a.Id)).Select(a => a.Id).ToListAsync(cancellationToken);
                if (all.Count != request.AutoriIds.Count) return Result.Failure("Alcuni autori non esistono");
            }

            libro.ImpostaDati(request.Titolo, request.Isbn, request.Prezzo, request.DataPubblicazione, request.CollanaId);
            libro.ImpostaAutori(request.AutoriIds ?? Array.Empty<int>());
            if (request.Attivo) libro.Attiva(); else libro.Disattiva();

            await _db.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (DomainException ex)
        {
            return Result.Failure(ex.Message);
        }
    }

    public async Task<Result> DeleteLibroAsync(int id, CancellationToken cancellationToken = default)
    {
        var libro = await _db.Libri.FindAsync(new object[] { id }, cancellationToken);
        if (libro == null) return Result.Failure("Libro non trovato");
        libro.Disattiva();
        await _db.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
