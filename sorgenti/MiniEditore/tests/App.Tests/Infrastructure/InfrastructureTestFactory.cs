using App.Domain.Catalogo;
using App.Domain.Clienti;
using App.Domain.Documenti;
using App.Infrastructure.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace App.Tests.Infrastructure;

public sealed class InfrastructureTestFactory : IDisposable
{
    private readonly SqliteConnection _connection;

    public InfrastructureTestFactory()
    {
        _connection = new SqliteConnection("Data Source=:memory:");
        _connection.Open();
        DbContext = CreateDbContext();
        DbContext.Database.EnsureCreated();
    }

    public AppDbContext DbContext { get; }

    public AppDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(_connection)
            .Options;

        return new AppDbContext(options);
    }

    public async Task<(Cliente Cliente, Libro Libro, Documento Documento)> SeedDocumentoAsync(bool confermato = false)
    {
        var cliente = new Cliente("Cliente Test");
        var libro = new Libro("Libro Test", null, 10m);
        await DbContext.Clienti.AddAsync(cliente);
        await DbContext.Libri.AddAsync(libro);
        await DbContext.SaveChangesAsync();

        var documento = new Documento($"DOC-{Guid.NewGuid():N}", new DateOnly(2026, 1, 1), cliente.Id, TipoDocumento.Vendita);
        documento.AggiungiRiga(new DocumentoRiga(libro.Id, libro.Titolo, 2, libro.Prezzo, 0m));
        if (confermato)
        {
            documento.Conferma();
        }

        await DbContext.Documenti.AddAsync(documento);
        await DbContext.SaveChangesAsync();
        return (cliente, libro, documento);
    }

    public void Dispose()
    {
        DbContext.Dispose();
        _connection.Dispose();
    }
}
