using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using App.Infrastructure.Background;

namespace App.Api.Endpoints;

public static class BackgroundEndpoints
{
    public sealed record GenerateReportRequest(DateOnly DataDa, DateOnly DataA);

    public static IEndpointRouteBuilder MapBackgroundEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/background").WithTags("Background");

        group.MapPost("/report-vendite", async (GenerateReportRequest request, IBackgroundTaskQueue q) =>
        {
            await q.QueueAsync(new GenerateSalesReportJob(request.DataDa, request.DataA));
            return Results.Accepted();
        }).WithName("QueueGenerateSalesReport");

        group.MapPost("/recalculate-dashboard", async (IBackgroundTaskQueue q) =>
        {
            await q.QueueAsync(new RecalculateDashboardJob());
            return Results.Accepted();
        }).WithName("QueueRecalculateDashboard");

        return app;
    }
}
