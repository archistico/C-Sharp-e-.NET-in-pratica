using FluentAssertions;
using System.Net;

namespace App.Tests.Api;

public class ReportEndpointsTests : IClassFixture<ApiTestFactory>
{
    private readonly HttpClient _client;

    public ReportEndpointsTests(ApiTestFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Theory]
    [InlineData("/api/report/vendite")]
    [InlineData("/api/report/vendite/libri")]
    [InlineData("/api/report/vendite/clienti")]
    [InlineData("/api/report/vendite/mensile")]
    public async Task GET_report_con_date_valide_restituisce_200(string path)
    {
        var response = await _client.GetAsync($"{path}?dataDa=2026-01-01&dataA=2026-12-31");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GET_api_report_vendite_con_date_non_valide_restituisce_400()
    {
        var response = await _client.GetAsync("/api/report/vendite?dataDa=2026-02-01&dataA=2026-01-01");

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
