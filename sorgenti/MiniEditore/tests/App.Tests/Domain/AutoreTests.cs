using App.Domain.Catalogo;
using App.Domain.Common;
using FluentAssertions;

namespace App.Tests.Domain;

public class AutoreTests
{
    [Fact]
    public void Crea_autore_valido()
    {
        var autore = new Autore(" Ada ", " Lovelace ", " ", null);

        autore.Nome.Should().Be("Ada");
        autore.Cognome.Should().Be("Lovelace");
        autore.Email.Should().BeNull();
    }

    [Fact]
    public void Autore_senza_nome_e_cognome_lancia_DomainException()
    {
        Action act = () => new Autore(" ", " ");

        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void NomeVisualizzato_compone_nome_e_cognome()
    {
        var autore = new Autore("Ada", "Lovelace");

        autore.NomeVisualizzato.Should().Be("Ada Lovelace");
    }
}
