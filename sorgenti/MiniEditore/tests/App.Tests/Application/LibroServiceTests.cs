using App.Application.Catalogo.Libri;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace App.Tests.Application;

public class LibroServiceTests
{
    [Fact]
    public async Task CreateLibroAsync_crea_libro_valido()
    {
        using var factory = new ApplicationTestFactory();
        var service = factory.CreateLibroService();

        var result = await service.CreateLibroAsync(new CreateLibroRequest("Libro", "ISBN-1", 12m, null, null, Array.Empty<int>()));

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeGreaterThan(0);
        (await factory.DbContext.Libri.AnyAsync(x => x.Id == result.Value)).Should().BeTrue();
    }

    [Fact]
    public async Task CreateLibroAsync_rifiuta_isbn_duplicato()
    {
        using var factory = new ApplicationTestFactory();
        var service = factory.CreateLibroService();
        await service.CreateLibroAsync(new CreateLibroRequest("Libro 1", "ISBN-1", 12m, null, null, Array.Empty<int>()));

        var result = await service.CreateLibroAsync(new CreateLibroRequest("Libro 2", "ISBN-1", 15m, null, null, Array.Empty<int>()));

        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateLibroAsync_modifica_libro()
    {
        using var factory = new ApplicationTestFactory();
        var service = factory.CreateLibroService();
        var created = await service.CreateLibroAsync(new CreateLibroRequest("Libro", "ISBN-1", 12m, null, null, Array.Empty<int>()));

        var result = await service.UpdateLibroAsync(created.Value, new UpdateLibroRequest("Libro modificato", null, 20m, null, null, Array.Empty<int>(), true));

        result.IsSuccess.Should().BeTrue();
        var libro = await factory.DbContext.Libri.FindAsync(created.Value);
        libro!.Titolo.Should().Be("Libro modificato");
        libro.Prezzo.Should().Be(20m);
    }

    [Fact]
    public async Task DeleteLibroAsync_disattiva_libro()
    {
        using var factory = new ApplicationTestFactory();
        var service = factory.CreateLibroService();
        var created = await service.CreateLibroAsync(new CreateLibroRequest("Libro", null, 12m, null, null, Array.Empty<int>()));

        var result = await service.DeleteLibroAsync(created.Value);

        result.IsSuccess.Should().BeTrue();
        var libro = await factory.DbContext.Libri.FindAsync(created.Value);
        libro!.Attivo.Should().BeFalse();
    }
}
