using App.Domain.Catalogo;
using App.Domain.Clienti;
using App.Domain.Documenti;
using App.Infrastructure.Documenti;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace App.Tests.Infrastructure;

public class DocumentoRepositoryTests
{
    [Fact]
    public async Task AddAsync_salva_documento()
    {
        using var factory = new InfrastructureTestFactory();
        var cliente = new Cliente("Cliente");
        var libro = new Libro("Libro", null, 10m);
        await factory.DbContext.Clienti.AddAsync(cliente);
        await factory.DbContext.Libri.AddAsync(libro);
        await factory.DbContext.SaveChangesAsync();

        var repository = new DocumentoRepository(factory.DbContext);
        var documento = new Documento("DOC-TEST", new DateOnly(2026, 1, 1), cliente.Id, TipoDocumento.Vendita);
        documento.AggiungiRiga(new DocumentoRiga(libro.Id, libro.Titolo, 1, 10m, 0m));

        await repository.AddAsync(documento);
        await repository.SaveChangesAsync();

        (await factory.DbContext.Documenti.CountAsync()).Should().Be(1);
    }

    [Fact]
    public async Task GetByIdAsync_carica_documento_con_righe()
    {
        using var factory = new InfrastructureTestFactory();
        var seeded = await factory.SeedDocumentoAsync();
        var repository = new DocumentoRepository(factory.DbContext);

        var documento = await repository.GetByIdAsync(seeded.Documento.Id);

        documento.Should().NotBeNull();
        documento!.Righe.Should().ContainSingle();
    }
}
