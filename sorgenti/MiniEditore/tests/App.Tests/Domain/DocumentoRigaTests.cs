using App.Domain.Common;
using App.Domain.Documenti;
using FluentAssertions;

namespace App.Tests.Domain;

public class DocumentoRigaTests
{
    [Fact]
    public void Crea_riga_valida()
    {
        var riga = new DocumentoRiga(1, " Libro ", 2, 10m, 10m);

        riga.Descrizione.Should().Be("Libro");
        riga.TotaleLordo.Should().Be(20m);
        riga.TotaleRiga.Should().Be(18m);
    }

    [Theory]
    [InlineData(0, "Libro", 1, 10, 0)]
    [InlineData(1, "", 1, 10, 0)]
    [InlineData(1, "Libro", 0, 10, 0)]
    [InlineData(1, "Libro", 1, -1, 0)]
    [InlineData(1, "Libro", 1, 10, 101)]
    public void Riga_non_valida_lancia_DomainException(int libroId, string descrizione, int quantita, decimal prezzo, decimal sconto)
    {
        Action act = () => new DocumentoRiga(libroId, descrizione, quantita, prezzo, sconto);

        act.Should().Throw<DomainException>();
    }
}
