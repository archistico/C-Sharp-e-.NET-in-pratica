using App.Application.Catalogo.Libri;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace App.Tests.Api;

public class CatalogoEndpointsTests : IClassFixture<ApiTestFactory>
{
    private readonly HttpClient _client;

    public CatalogoEndpointsTests(ApiTestFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GET_api_catalogo_libri_restituisce_200()
    {
        var response = await _client.GetAsync("/api/catalogo/libri");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task POST_api_catalogo_libri_crea_libro()
    {
        var request = new CreateLibroRequest($"Libro API {Guid.NewGuid():N}", $"ISBN-{Guid.NewGuid():N}", 19.90m, null, null, Array.Empty<int>());

        var response = await _client.PostAsJsonAsync("/api/catalogo/libri", request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var created = await response.Content.ReadFromJsonAsync<CreatedResponse>();
        created!.Id.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task GET_api_catalogo_libri_id_restituisce_dettaglio()
    {
        var created = await CreateLibroAsync();

        var response = await _client.GetAsync($"/api/catalogo/libri/{created.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task DELETE_api_catalogo_libri_id_disattiva_libro()
    {
        var created = await CreateLibroAsync();

        var response = await _client.DeleteAsync($"/api/catalogo/libri/{created.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task GET_api_catalogo_autori_restituisce_200()
    {
        var response = await _client.GetAsync("/api/catalogo/autori");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GET_api_catalogo_collane_restituisce_200()
    {
        var response = await _client.GetAsync("/api/catalogo/collane");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    private async Task<CreatedResponse> CreateLibroAsync()
    {
        var request = new CreateLibroRequest($"Libro API {Guid.NewGuid():N}", $"ISBN-{Guid.NewGuid():N}", 19.90m, null, null, Array.Empty<int>());
        var response = await _client.PostAsJsonAsync("/api/catalogo/libri", request);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<CreatedResponse>())!;
    }

    private sealed record CreatedResponse(int Id);
}
