using App.Domain.Common;
using App.Domain.Documenti;
using FluentAssertions;

namespace App.Tests.Domain;

public class DocumentoTests
{
    [Fact]
    public void Documento_nuovo_nasce_in_bozza()
    {
        var documento = CreaDocumento();

        documento.Stato.Should().Be(StatoDocumento.Bozza);
    }

    [Fact]
    public void Documento_senza_numero_lancia_DomainException()
    {
        Action act = () => new Documento(" ", DateOnly.FromDateTime(DateTime.Today), 1, TipoDocumento.Vendita);

        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void AggiungiRiga_calcola_totali()
    {
        var documento = CreaDocumento();

        documento.AggiungiRiga(new DocumentoRiga(1, "Libro", 2, 10m, 10m));

        documento.TotaleImponibile.Should().Be(20m);
        documento.TotaleSconto.Should().Be(2m);
        documento.TotaleDocumento.Should().Be(18m);
    }

    [Fact]
    public void Documento_senza_righe_non_puo_essere_confermato()
    {
        var documento = CreaDocumento();

        Action act = documento.Conferma;

        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void Documento_con_righe_puo_essere_confermato()
    {
        var documento = CreaDocumentoConRiga();

        documento.Conferma();

        documento.Stato.Should().Be(StatoDocumento.Confermato);
    }

    [Fact]
    public void Documento_confermato_non_puo_essere_modificato()
    {
        var documento = CreaDocumentoConRiga();
        documento.Conferma();

        Action act = () => documento.AggiungiRiga(new DocumentoRiga(1, "Altro", 1, 5m, 0m));

        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void RimuoviRiga_ricalcola_totali()
    {
        var documento = CreaDocumento();
        documento.AggiungiRiga(new DocumentoRiga(1, "A", 1, 10m, 0m));
        documento.AggiungiRiga(new DocumentoRiga(2, "B", 1, 20m, 0m));

        documento.RimuoviRiga(0);

        documento.TotaleDocumento.Should().Be(20m);
    }

    [Fact]
    public void ModificaRiga_con_indice_non_valido_lancia_DomainException()
    {
        var documento = CreaDocumentoConRiga();

        Action act = () => documento.ModificaRiga(99, 1, "Libro", 1, 10m, 0m);

        act.Should().Throw<DomainException>();
    }

    private static Documento CreaDocumento() => new("DOC-001", new DateOnly(2026, 1, 1), 1, TipoDocumento.Vendita);

    private static Documento CreaDocumentoConRiga()
    {
        var documento = CreaDocumento();
        documento.AggiungiRiga(new DocumentoRiga(1, "Libro", 1, 10m, 0m));
        return documento;
    }
}
