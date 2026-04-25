using App.Application.Clienti;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace App.Tests.Api;

public class ClientiEndpointsTests : IClassFixture<ApiTestFactory>
{
    private readonly HttpClient _client;

    public ClientiEndpointsTests(ApiTestFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GET_api_clienti_restituisce_200()
    {
        var response = await _client.GetAsync("/api/clienti");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task POST_api_clienti_crea_cliente()
    {
        var response = await _client.PostAsJsonAsync("/api/clienti", new CreateClienteRequest($"Cliente {Guid.NewGuid():N}", null, null, null, null));

        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task PUT_api_clienti_id_modifica_cliente()
    {
        var created = await CreateClienteAsync();

        var response = await _client.PutAsJsonAsync($"/api/clienti/{created.Id}", new UpdateClienteRequest("Cliente Modificato", null, null, null, null, true));

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    private async Task<CreatedResponse> CreateClienteAsync()
    {
        var response = await _client.PostAsJsonAsync("/api/clienti", new CreateClienteRequest($"Cliente {Guid.NewGuid():N}", null, null, null, null));
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<CreatedResponse>())!;
    }

    private sealed record CreatedResponse(int Id);
}
