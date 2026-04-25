using global::App.Web.Models;
using global::App.Application.Catalogo.Collane;
using global::App.Application.Catalogo.Autori;
using global::App.Application.Catalogo.Libri;
using System.Net.Http.Json;
using System.Collections.Generic;

namespace App.Web.Services;

public class CatalogoApiClient
{
    private readonly HttpClient _http;

    public CatalogoApiClient(HttpClient http) => _http = http;

    public async Task<IReadOnlyList<LibroListItemDto>> GetLibriAsync()
        => await _http.GetFromJsonAsync<List<LibroListItemDto>>("/api/catalogo/libri") ?? new List<LibroListItemDto>();

    public async Task<LibroDetailDto?> GetLibroAsync(int id)
        => await _http.GetFromJsonAsync<LibroDetailDto>($"/api/catalogo/libri/{id}");

    public async Task<int?> CreateLibroAsync(CreateLibroRequest request)
    {
        var res = await _http.PostAsJsonAsync("/api/catalogo/libri", request);
        if (res.IsSuccessStatusCode)
        {
            var obj = await res.Content.ReadFromJsonAsync<CreatedResponse>();
            return obj?.Id;
        }
        return null;
    }

    public async Task<UiResult> UpdateLibroAsync(int id, UpdateLibroRequest request)
    {
        var res = await _http.PutAsJsonAsync($"/api/catalogo/libri/{id}", request);
        if (res.IsSuccessStatusCode) return UiResult.Ok();
        var err = await ReadErrorAsync(res);
        return UiResult.Fail(err);
    }

    public async Task<UiResult> DeleteLibroAsync(int id)
    {
        var res = await _http.DeleteAsync($"/api/catalogo/libri/{id}");
        if (res.IsSuccessStatusCode) return UiResult.Ok();
        var err = await ReadErrorAsync(res);
        return UiResult.Fail(err);
    }

    // Autori
    public async Task<IReadOnlyList<AutoreDto>> GetAutoriAsync()
        => await _http.GetFromJsonAsync<List<AutoreDto>>("/api/catalogo/autori") ?? new List<AutoreDto>();

    public async Task<AutoreDto?> GetAutoreAsync(int id)
        => await _http.GetFromJsonAsync<AutoreDto>($"/api/catalogo/autori/{id}");

    public async Task<int?> CreateAutoreAsync(CreateAutoreRequest request)
    {
        var res = await _http.PostAsJsonAsync("/api/catalogo/autori", request);
        if (res.IsSuccessStatusCode) return (await res.Content.ReadFromJsonAsync<CreatedResponse>())?.Id;
        return null;
    }

    public async Task<UiResult> UpdateAutoreAsync(int id, UpdateAutoreRequest request)
    {
        var res = await _http.PutAsJsonAsync($"/api/catalogo/autori/{id}", request);
        if (res.IsSuccessStatusCode) return UiResult.Ok();
        var err = await ReadErrorAsync(res);
        return UiResult.Fail(err);
    }

    // Collane
    public async Task<IReadOnlyList<CollanaDto>> GetCollaneAsync()
        => await _http.GetFromJsonAsync<List<CollanaDto>>("/api/catalogo/collane") ?? new List<CollanaDto>();

    public async Task<CollanaDto?> GetCollanaAsync(int id)
        => await _http.GetFromJsonAsync<CollanaDto>($"/api/catalogo/collane/{id}");

    public async Task<int?> CreateCollanaAsync(CreateCollanaRequest request)
    {
        var res = await _http.PostAsJsonAsync("/api/catalogo/collane", request);
        if (res.IsSuccessStatusCode) return (await res.Content.ReadFromJsonAsync<CreatedResponse>())?.Id;
        return null;
    }

    public async Task<UiResult> UpdateCollanaAsync(int id, UpdateCollanaRequest request)
    {
        var res = await _http.PutAsJsonAsync($"/api/catalogo/collane/{id}", request);
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
