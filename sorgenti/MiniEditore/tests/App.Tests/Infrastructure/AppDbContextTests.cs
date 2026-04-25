using App.Domain.Catalogo;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace App.Tests.Infrastructure;

public class AppDbContextTests
{
    [Fact]
    public void AppDbContext_crea_database()
    {
        using var factory = new InfrastructureTestFactory();

        factory.DbContext.Database.CanConnect().Should().BeTrue();
    }

    [Fact]
    public async Task Salva_e_legge_libro()
    {
        using var factory = new InfrastructureTestFactory();
        await factory.DbContext.Libri.AddAsync(new Libro("Libro", "ISBN", 10m));
        await factory.DbContext.SaveChangesAsync();

        var libro = await factory.DbContext.Libri.SingleAsync();

        libro.Titolo.Should().Be("Libro");
    }

    [Fact]
    public async Task Salva_documento_con_righe()
    {
        using var factory = new InfrastructureTestFactory();

        var seeded = await factory.SeedDocumentoAsync();

        var documento = await factory.DbContext.Documenti.Include(x => x.Righe).SingleAsync(x => x.Id == seeded.Documento.Id);
        documento.Righe.Should().ContainSingle();
    }
}
