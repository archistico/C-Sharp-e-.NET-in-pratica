using App.Application.Catalogo.Libri;
using App.Application.Clienti;
using App.Application.Dashboard;
using App.Application.Report;
using App.Domain.Catalogo;
using App.Domain.Clienti;
using App.Domain.Documenti;
using App.Infrastructure.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace App.Tests.Application;

public sealed class ApplicationTestFactory : IDisposable
{
    private readonly SqliteConnection _connection;

    public ApplicationTestFactory()
    {
        _connection = new SqliteConnection("Data Source=:memory:");
        _connection.Open();
        DbContext = CreateDbContext();
        DbContext.Database.EnsureCreated();
    }

    public AppDbContext DbContext { get; }
    public TestClock Clock { get; } = new();

    public AppDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(_connection)
            .Options;

        return new AppDbContext(options);
    }

    public LibroService CreateLibroService() => new(DbContext);
    public ClienteService CreateClienteService() => new(DbContext);
    public DashboardService CreateDashboardService() => new(DbContext, Clock);
    public ReportService CreateReportService() => new(DbContext);

    public async Task<(Cliente Cliente, Libro Libro)> SeedClienteELibroAsync()
    {
        var cliente = new Cliente("Cliente Test");
        var libro = new Libro("Libro Test", "ISBN-TEST", 20m);
        await DbContext.Clienti.AddAsync(cliente);
        await DbContext.Libri.AddAsync(libro);
        await DbContext.SaveChangesAsync();
        return (cliente, libro);
    }

    public async Task<Documento> SeedDocumentoConfermatoAsync(DateOnly? data = null)
    {
        var (cliente, libro) = await SeedClienteELibroAsync();
        var documento = new Documento($"DOC-{Guid.NewGuid():N}", data ?? Clock.Today, cliente.Id, TipoDocumento.Vendita);
        documento.AggiungiRiga(new DocumentoRiga(libro.Id, libro.Titolo, 2, libro.Prezzo, 0m));
        documento.Conferma();
        await DbContext.Documenti.AddAsync(documento);
        await DbContext.SaveChangesAsync();
        return documento;
    }

    public void Dispose()
    {
        DbContext.Dispose();
        _connection.Dispose();
    }
}
