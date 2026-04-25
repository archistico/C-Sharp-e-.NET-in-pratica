using global::App.Web.Models;
using global::App.Application.Documenti;
using System.Net.Http.Json;
using System.Collections.Generic;

namespace App.Web.Services;

public class DocumentiApiClient
{
    private readonly HttpClient _http;
    public DocumentiApiClient(HttpClient http) => _http = http;

    public async Task<IReadOnlyList<DocumentoListItemDto>> GetDocumentiAsync()
        => await _http.GetFromJsonAsync<List<DocumentoListItemDto>>("/api/documenti") ?? new List<DocumentoListItemDto>();

    public async Task<DocumentoDetailDto?> GetDocumentoAsync(int id)
        => await _http.GetFromJsonAsync<DocumentoDetailDto>($"/api/documenti/{id}");

    public async Task<int?> CreateDocumentoAsync(CreateDocumentoRequest request)
    {
        var res = await _http.PostAsJsonAsync("/api/documenti", request);
        if (res.IsSuccessStatusCode) return (await res.Content.ReadFromJsonAsync<CreatedResponse>())?.Id;
        return null;
    }

    public async Task<UiResult> ConfermaDocumentoAsync(int id)
    {
        var res = await _http.PostAsync($"/api/documenti/{id}/conferma", null);
        if (res.IsSuccessStatusCode) return UiResult.Ok();
        var err = await ReadErrorAsync(res);
        return UiResult.Fail(err);
    }

    public async Task<UiResult> AnnullaDocumentoAsync(int id)
    {
        var res = await _http.PostAsync($"/api/documenti/{id}/annulla", null);
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
