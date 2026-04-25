using global::App.Web.Models;
using global::App.Application.Clienti;
using System.Net.Http.Json;
using System.Collections.Generic;

namespace App.Web.Services;

public class ClientiApiClient
{
    private readonly HttpClient _http;
    public ClientiApiClient(HttpClient http) => _http = http;

    public async Task<IReadOnlyList<ClienteDto>> GetClientiAsync()
        => await _http.GetFromJsonAsync<List<ClienteDto>>("/api/clienti") ?? new List<ClienteDto>();

    public async Task<ClienteDto?> GetClienteAsync(int id)
        => await _http.GetFromJsonAsync<ClienteDto>($"/api/clienti/{id}");

    public async Task<int?> CreateClienteAsync(CreateClienteRequest request)
    {
        var res = await _http.PostAsJsonAsync("/api/clienti", request);
        if (res.IsSuccessStatusCode) return (await res.Content.ReadFromJsonAsync<CreatedResponse>())?.Id;
        return null;
    }

    public async Task<UiResult> UpdateClienteAsync(int id, UpdateClienteRequest request)
    {
        var res = await _http.PutAsJsonAsync($"/api/clienti/{id}", request);
        if (res.IsSuccessStatusCode) return UiResult.Ok();
        var err = await ReadErrorAsync(res);
        return UiResult.Fail(err);
    }

    private record CreatedResponse { public int Id { get; set; } }

    private static async Task<string> ReadErrorAsync(HttpResponseMessage response)
    {
        try
        {
            var obj = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            if (obj != null && obj.TryGetValue("error", out var e)) return e;
        }
        catch { }

        var txt = await response.Content.ReadAsStringAsync();
        if (!string.IsNullOrWhiteSpace(txt)) return txt;
        return response.ReasonPhrase ?? "Errore HTTP";
    }
}
