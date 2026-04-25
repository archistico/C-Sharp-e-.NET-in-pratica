using App.Application.Catalogo.Libri;
using App.Application.Clienti;
using App.Application.Documenti;
using App.Domain.Documenti;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace App.Tests.Api;

public class DocumentiEndpointsTests : IClassFixture<ApiTestFactory>
{
    private readonly HttpClient _client;

    public DocumentiEndpointsTests(ApiTestFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GET_api_documenti_restituisce_200()
    {
        var response = await _client.GetAsync("/api/documenti");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task POST_api_documenti_crea_documento()
    {
        var response = await CreateDocumentoResponseAsync();

        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task GET_api_documenti_id_restituisce_dettaglio()
    {
        var created = await CreateDocumentoAsync();

        var response = await _client.GetAsync($"/api/documenti/{created.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task POST_api_documenti_id_conferma_conferma_documento()
    {
        var created = await CreateDocumentoAsync();

        var response = await _client.PostAsync($"/api/documenti/{created.Id}/conferma", null);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task POST_api_documenti_id_annulla_annulla_documento()
    {
        var created = await CreateDocumentoAsync();

        var response = await _client.PostAsync($"/api/documenti/{created.Id}/annulla", null);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    private async Task<HttpResponseMessage> CreateDocumentoResponseAsync()
    {
        var cliente = await CreateClienteAsync();
        var libro = await CreateLibroAsync();
        var request = new CreateDocumentoRequest(
            new DateOnly(2026, 1, 1),
            cliente.Id,
            TipoDocumento.Vendita,
            new[] { new CreateDocumentoRigaRequest(libro.Id, 2, 10m, 0m) });

        return await _client.PostAsJsonAsync("/api/documenti", request);
    }

    private async Task<CreatedResponse> CreateDocumentoAsync()
    {
        var response = await CreateDocumentoResponseAsync();
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<CreatedResponse>())!;
    }

    private async Task<CreatedResponse> CreateClienteAsync()
    {
        var response = await _client.PostAsJsonAsync("/api/clienti", new CreateClienteRequest($"Cliente {Guid.NewGuid():N}", null, null, null, null));
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<CreatedResponse>())!;
    }

    private async Task<CreatedResponse> CreateLibroAsync()
    {
        var response = await _client.PostAsJsonAsync("/api/catalogo/libri", new CreateLibroRequest($"Libro {Guid.NewGuid():N}", $"ISBN-{Guid.NewGuid():N}", 10m, null, null, Array.Empty<int>()));
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<CreatedResponse>())!;
    }

    private sealed record CreatedResponse(int Id);
}
