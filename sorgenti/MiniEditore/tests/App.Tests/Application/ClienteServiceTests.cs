using App.Application.Clienti;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace App.Tests.Application;

public class ClienteServiceTests
{
    [Fact]
    public async Task CreateClienteAsync_crea_cliente_valido()
    {
        using var factory = new ApplicationTestFactory();
        var service = factory.CreateClienteService();

        var result = await service.CreateClienteAsync(new CreateClienteRequest("Cliente", "c@test.it", null, null, null));

        result.IsSuccess.Should().BeTrue();
        (await factory.DbContext.Clienti.AnyAsync(x => x.Id == result.Value)).Should().BeTrue();
    }

    [Fact]
    public async Task UpdateClienteAsync_modifica_cliente()
    {
        using var factory = new ApplicationTestFactory();
        var service = factory.CreateClienteService();
        var created = await service.CreateClienteAsync(new CreateClienteRequest("Cliente", null, null, null, null));

        var result = await service.UpdateClienteAsync(created.Value, new UpdateClienteRequest("Cliente Modificato", null, null, null, null, false));

        result.IsSuccess.Should().BeTrue();
        var cliente = await factory.DbContext.Clienti.FindAsync(created.Value);
        cliente!.RagioneSociale.Should().Be("Cliente Modificato");
        cliente.Attivo.Should().BeFalse();
    }

    [Fact]
    public async Task UpdateClienteAsync_su_cliente_inesistente_fallisce()
    {
        using var factory = new ApplicationTestFactory();
        var service = factory.CreateClienteService();

        var result = await service.UpdateClienteAsync(999, new UpdateClienteRequest("Cliente", null, null, null, null, true));

        result.IsFailure.Should().BeTrue();
    }
}
