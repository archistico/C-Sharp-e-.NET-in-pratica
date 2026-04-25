using global::App.Application.Report;
using System.Net.Http.Json;
using System.Collections.Generic;

namespace App.Web.Services;

public class ReportApiClient
{
    private readonly HttpClient _http;
    public ReportApiClient(HttpClient http) => _http = http;

    public async Task<IReadOnlyList<VenditePerLibroDto>> GetVenditePerLibroAsync(DateOnly dataDa, DateOnly dataA)
    {
        var qs = $"?dataDa={dataDa:yyyy-MM-dd}&dataA={dataA:yyyy-MM-dd}";
        return await _http.GetFromJsonAsync<List<VenditePerLibroDto>>("/api/report/vendite/libri" + qs) ?? new List<VenditePerLibroDto>();
    }

    public async Task<IReadOnlyList<VenditePerClienteDto>> GetVenditePerClienteAsync(DateOnly dataDa, DateOnly dataA)
    {
        var qs = $"?dataDa={dataDa:yyyy-MM-dd}&dataA={dataA:yyyy-MM-dd}";
        return await _http.GetFromJsonAsync<List<VenditePerClienteDto>>("/api/report/vendite/clienti" + qs) ?? new List<VenditePerClienteDto>();
    }

    public async Task<IReadOnlyList<RiepilogoMensileDto>> GetRiepilogoMensileAsync(DateOnly dataDa, DateOnly dataA)
    {
        var qs = $"?dataDa={dataDa:yyyy-MM-dd}&dataA={dataA:yyyy-MM-dd}";
        return await _http.GetFromJsonAsync<List<RiepilogoMensileDto>>("/api/report/vendite/mensile" + qs) ?? new List<RiepilogoMensileDto>();
    }
}
