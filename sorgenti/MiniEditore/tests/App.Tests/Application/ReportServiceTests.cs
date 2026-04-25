using App.Application.Report;
using FluentAssertions;

namespace App.Tests.Application;

public class ReportServiceTests
{
    [Fact]
    public async Task GetVenditePerLibroAsync_restituisce_solo_documenti_confermati()
    {
        using var factory = new ApplicationTestFactory();
        await factory.SeedDocumentoConfermatoAsync(new DateOnly(2026, 1, 10));
        var service = factory.CreateReportService();

        var result = await service.GetVenditePerLibroAsync(new ReportVenditeRequest(new DateOnly(2026, 1, 1), new DateOnly(2026, 1, 31)));

        result.Should().ContainSingle();
        result[0].TotaleVenduto.Should().Be(40m);
    }

    [Fact]
    public async Task GetVenditePerClienteAsync_raggruppa_per_cliente()
    {
        using var factory = new ApplicationTestFactory();
        await factory.SeedDocumentoConfermatoAsync(new DateOnly(2026, 1, 10));
        var service = factory.CreateReportService();

        var result = await service.GetVenditePerClienteAsync(new ReportVenditeRequest(new DateOnly(2026, 1, 1), new DateOnly(2026, 1, 31)));

        result.Should().ContainSingle();
        result[0].TotaleVenduto.Should().Be(40m);
    }

    [Fact]
    public async Task GetRiepilogoMensileAsync_raggruppa_per_mese()
    {
        using var factory = new ApplicationTestFactory();
        await factory.SeedDocumentoConfermatoAsync(new DateOnly(2026, 1, 10));
        var service = factory.CreateReportService();

        var result = await service.GetRiepilogoMensileAsync(new ReportVenditeRequest(new DateOnly(2026, 1, 1), new DateOnly(2026, 12, 31)));

        result.Should().ContainSingle();
        result[0].Anno.Should().Be(2026);
        result[0].Mese.Should().Be(1);
    }

    [Fact]
    public async Task Report_con_dataDa_maggiore_di_dataA_lancia_ArgumentException()
    {
        using var factory = new ApplicationTestFactory();
        var service = factory.CreateReportService();

        Func<Task> act = () => service.GetVenditePerLibroAsync(new ReportVenditeRequest(new DateOnly(2026, 2, 1), new DateOnly(2026, 1, 1)));

        await act.Should().ThrowAsync<ArgumentException>();
    }
}
