using App.Application.Dashboard;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace App.Api.Endpoints;

public static class DashboardEndpoints
{
    public static IEndpointRouteBuilder MapDashboardEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("/api/dashboard").WithTags("Dashboard");

        group.MapGet("/summary", async (IDashboardService service, CancellationToken cancellationToken) =>
        {
            DashboardSummaryDto dto = await service.GetSummaryAsync(cancellationToken);
            return Results.Ok(dto);
        }).WithName("GetDashboardSummary");

        return app;
    }
}
