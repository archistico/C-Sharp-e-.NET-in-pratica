using App.Application.Catalogo.Libri;
using App.Application.Catalogo.Autori;
using App.Application.Catalogo.Collane;
using App.Api.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace App.Api.Endpoints;

public static class CatalogoEndpoints
{
    public static IEndpointRouteBuilder MapCatalogoEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/catalogo").WithTags("Catalogo");

        // Libri
        group.MapGet("/libri", async (ILibroService svc) => await svc.GetLibriAsync())
            .WithName("GetLibri").WithSummary("Restituisce l'elenco dei libri.");

        group.MapGet("/libri/{id:int}", async (int id, ILibroService svc) =>
        {
            var r = await svc.GetLibroAsync(id);
            return r.ToHttpResult();
        }).WithName("GetLibro").WithSummary("Restituisce dettaglio libro.");

        group.MapPost("/libri", async (CreateLibroRequest req, ILibroService svc) =>
        {
            var r = await svc.CreateLibroAsync(req);
            return r.ToCreatedResult("/api/catalogo/libri");
        }).WithName("CreateLibro").WithSummary("Crea un libro.");

        group.MapPut("/libri/{id:int}", async (int id, UpdateLibroRequest req, ILibroService svc) =>
        {
            var r = await svc.UpdateLibroAsync(id, req);
            return r.ToHttpResult();
        }).WithName("UpdateLibro");

        group.MapDelete("/libri/{id:int}", async (int id, ILibroService svc) =>
        {
            var r = await svc.DeleteLibroAsync(id);
            return r.ToHttpResult();
        }).WithName("DeleteLibro");

        // Autori
        group.MapGet("/autori", async (IAutoreService svc) => await svc.GetAutoriAsync())
            .WithName("GetAutori");

        group.MapGet("/autori/{id:int}", async (int id, IAutoreService svc) => (await svc.GetAutoreAsync(id)).ToHttpResult())
            .WithName("GetAutore");

        group.MapPost("/autori", async (CreateAutoreRequest req, IAutoreService svc) =>
        {
            var r = await svc.CreateAutoreAsync(req);
            return r.ToCreatedResult("/api/catalogo/autori");
        }).WithName("CreateAutore");

        group.MapPut("/autori/{id:int}", async (int id, UpdateAutoreRequest req, IAutoreService svc) =>
            (await svc.UpdateAutoreAsync(id, req)).ToHttpResult()).WithName("UpdateAutore");

        // Collane
        group.MapGet("/collane", async (ICollanaService svc) => await svc.GetCollaneAsync()).WithName("GetCollane");

        group.MapGet("/collane/{id:int}", async (int id, ICollanaService svc) => (await svc.GetCollanaAsync(id)).ToHttpResult()).WithName("GetCollana");

        group.MapPost("/collane", async (CreateCollanaRequest req, ICollanaService svc) =>
        {
            var r = await svc.CreateCollanaAsync(req);
            return r.ToCreatedResult("/api/catalogo/collane");
        }).WithName("CreateCollana");

        group.MapPut("/collane/{id:int}", async (int id, UpdateCollanaRequest req, ICollanaService svc) => (await svc.UpdateCollanaAsync(id, req)).ToHttpResult()).WithName("UpdateCollana");

        return app;
    }
}
