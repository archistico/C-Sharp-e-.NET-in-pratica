using App.Application.Report;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace App.Api.Endpoints;

public static class ReportEndpoints
{
    public static IEndpointRouteBuilder MapReportEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("/api/report").WithTags("Report");

        group.MapGet("/vendite/libri", async (DateOnly? dataDa, DateOnly? dataA, IReportService service, CancellationToken cancellationToken) =>
        {
            IResult? validation = Validate(dataDa, dataA, out ReportVenditeRequest? request);
            if (validation is not null) return validation;
            return Results.Ok(await service.GetVenditePerLibroAsync(request!, cancellationToken));
        }).WithName("GetVenditePerLibro");

        group.MapGet("/vendite/clienti", async (DateOnly? dataDa, DateOnly? dataA, IReportService service, CancellationToken cancellationToken) =>
        {
            IResult? validation = Validate(dataDa, dataA, out ReportVenditeRequest? request);
            if (validation is not null) return validation;
            return Results.Ok(await service.GetVenditePerClienteAsync(request!, cancellationToken));
        }).WithName("GetVenditePerCliente");

        group.MapGet("/vendite/mensile", async (DateOnly? dataDa, DateOnly? dataA, IReportService service, CancellationToken cancellationToken) =>
        {
            IResult? validation = Validate(dataDa, dataA, out ReportVenditeRequest? request);
            if (validation is not null) return validation;
            return Results.Ok(await service.GetRiepilogoMensileAsync(request!, cancellationToken));
        }).WithName("GetVenditeMensile");

        group.MapGet("/vendite", async (DateOnly? dataDa, DateOnly? dataA, IReportService service, CancellationToken cancellationToken) =>
        {
            IResult? validation = Validate(dataDa, dataA, out ReportVenditeRequest? request);
            if (validation is not null) return validation;
            return Results.Ok(await service.GetVenditePerLibroAsync(request!, cancellationToken));
        }).WithName("GetVendite");

        return app;
    }

    private static IResult? Validate(DateOnly? dataDa, DateOnly? dataA, out ReportVenditeRequest? request)
    {
        request = null;

        if (!dataDa.HasValue || !dataA.HasValue)
        {
            return Results.BadRequest(new { error = "dataDa e dataA obbligatorie" });
        }

        if (dataDa.Value > dataA.Value)
        {
            return Results.BadRequest(new { error = "dataDa non può essere maggiore di dataA" });
        }

        request = new ReportVenditeRequest(dataDa.Value, dataA.Value);
        return null;
    }
}
