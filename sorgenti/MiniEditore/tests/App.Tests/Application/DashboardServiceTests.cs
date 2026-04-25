using FluentAssertions;

namespace App.Tests.Application;

public class DashboardServiceTests
{
    [Fact]
    public async Task GetSummaryAsync_restituisce_conteggi()
    {
        using var factory = new ApplicationTestFactory();
        await factory.SeedDocumentoConfermatoAsync();
        var service = factory.CreateDashboardService();

        var summary = await service.GetSummaryAsync();

        summary.NumeroLibriAttivi.Should().Be(1);
        summary.NumeroClienti.Should().Be(1);
        summary.NumeroDocumentiMese.Should().Be(1);
        summary.TotaleDocumentiMese.Should().Be(40m);
    }

    [Fact]
    public async Task GetSummaryAsync_restituisce_libri_piu_venduti()
    {
        using var factory = new ApplicationTestFactory();
        await factory.SeedDocumentoConfermatoAsync();
        var service = factory.CreateDashboardService();

        var summary = await service.GetSummaryAsync();

        summary.LibriPiuVenduti.Should().ContainSingle();
        summary.LibriPiuVenduti[0].QuantitaVenduta.Should().Be(2);
    }
}
