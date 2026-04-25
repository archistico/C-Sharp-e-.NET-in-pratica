using global::App.Application.Dashboard;
using System.Net.Http.Json;

namespace App.Web.Services;

public class DashboardApiClient
{
    private readonly HttpClient _http;
    public DashboardApiClient(HttpClient http) => _http = http;

    public async Task<DashboardSummaryDto?> GetSummaryAsync()
        => await _http.GetFromJsonAsync<DashboardSummaryDto>("/api/dashboard/summary");
}
