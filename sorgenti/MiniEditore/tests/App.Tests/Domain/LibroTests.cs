using App.Domain.Catalogo;
using App.Domain.Common;
using FluentAssertions;

namespace App.Tests.Domain;

public class LibroTests
{
    [Fact]
    public void Crea_libro_valido()
    {
        var libro = new Libro(" Titolo ", " ISBN ", 10.50m, new DateOnly(2026, 1, 1));

        libro.Titolo.Should().Be("Titolo");
        libro.Isbn.Should().Be("ISBN");
        libro.Prezzo.Should().Be(10.50m);
        libro.Attivo.Should().BeTrue();
    }

    [Fact]
    public void Libro_con_titolo_vuoto_lancia_DomainException()
    {
        Action act = () => new Libro(" ", null, 10m);

        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void Libro_con_prezzo_negativo_lancia_DomainException()
    {
        Action act = () => new Libro("Titolo", null, -1m);

        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void Disattiva_libro_imposta_Attivo_false()
    {
        var libro = new Libro("Titolo", null, 10m);

        libro.Disattiva();

        libro.Attivo.Should().BeFalse();
    }

    [Fact]
    public void Isbn_vuoto_diventa_null()
    {
        var libro = new Libro("Titolo", " ", 10m);

        libro.Isbn.Should().BeNull();
    }

    [Fact]
    public void ImpostaAutori_rimuove_duplicati()
    {
        var libro = new Libro("Titolo", null, 10m);

        libro.ImpostaAutori(new[] { 1, 1, 2 });

        libro.Autori.Select(x => x.AutoreId).Should().Equal(1, 2);
    }
}
