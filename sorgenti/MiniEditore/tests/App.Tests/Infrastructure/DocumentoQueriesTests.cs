using App.Infrastructure.Documenti;
using FluentAssertions;

namespace App.Tests.Infrastructure;

public class DocumentoQueriesTests
{
    [Fact]
    public async Task GetDocumentiAsync_restituisce_lista_con_cliente()
    {
        using var factory = new InfrastructureTestFactory();
        await factory.SeedDocumentoAsync();
        var queries = new DocumentoQueries(factory.DbContext);

        var result = await queries.GetDocumentiAsync();

        result.Should().ContainSingle();
        result[0].Cliente.Should().Be("Cliente Test");
    }

    [Fact]
    public async Task GetDocumentoDetailAsync_restituisce_dettaglio_con_righe()
    {
        using var factory = new InfrastructureTestFactory();
        var seeded = await factory.SeedDocumentoAsync();
        var queries = new DocumentoQueries(factory.DbContext);

        var result = await queries.GetDocumentoDetailAsync(seeded.Documento.Id);

        result.Should().NotBeNull();
        result!.Righe.Should().ContainSingle();
    }

    [Fact]
    public async Task GetDocumentoDetailAsync_con_id_inesistente_restituisce_null()
    {
        using var factory = new InfrastructureTestFactory();
        var queries = new DocumentoQueries(factory.DbContext);

        var result = await queries.GetDocumentoDetailAsync(999);

        result.Should().BeNull();
    }
}
