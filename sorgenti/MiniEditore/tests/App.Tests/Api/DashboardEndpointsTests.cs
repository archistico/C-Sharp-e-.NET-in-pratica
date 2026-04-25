using FluentAssertions;
using System.Net;

namespace App.Tests.Api;

public class DashboardEndpointsTests : IClassFixture<ApiTestFactory>
{
    private readonly HttpClient _client;

    public DashboardEndpointsTests(ApiTestFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GET_api_dashboard_summary_restituisce_200()
    {
        var response = await _client.GetAsync("/api/dashboard/summary");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
