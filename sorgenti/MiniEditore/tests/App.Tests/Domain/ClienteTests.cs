using App.Domain.Clienti;
using App.Domain.Common;
using FluentAssertions;

namespace App.Tests.Domain;

public class ClienteTests
{
    [Fact]
    public void Crea_cliente_valido()
    {
        var cliente = new Cliente(" Cliente ", " ", " 123 ", null, null);

        cliente.RagioneSociale.Should().Be("Cliente");
        cliente.Email.Should().BeNull();
        cliente.Telefono.Should().Be("123");
        cliente.Attivo.Should().BeTrue();
    }

    [Fact]
    public void Cliente_senza_ragione_sociale_lancia_DomainException()
    {
        Action act = () => new Cliente(" ");

        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void DisattivaCliente_imposta_Attivo_false()
    {
        var cliente = new Cliente("Cliente");

        cliente.DisattivaCliente();

        cliente.Attivo.Should().BeFalse();
    }
}
