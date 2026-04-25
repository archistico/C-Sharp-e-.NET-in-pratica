using App.Application.Clienti;
using App.Api.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace App.Api.Endpoints;

public static class ClientiEndpoints
{
    public static IEndpointRouteBuilder MapClientiEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/clienti").WithTags("Clienti");

        group.MapGet("", async (IClienteService svc) => await svc.GetClientiAsync()).WithName("GetClienti");

        group.MapGet("/{id:int}", async (int id, IClienteService svc) => (await svc.GetClienteAsync(id)).ToHttpResult()).WithName("GetCliente");

        group.MapPost("", async (CreateClienteRequest req, IClienteService svc) =>
        {
            var r = await svc.CreateClienteAsync(req);
            return r.ToCreatedResult("/api/clienti");
        }).WithName("CreateCliente");

        group.MapPut("/{id:int}", async (int id, UpdateClienteRequest req, IClienteService svc) => (await svc.UpdateClienteAsync(id, req)).ToHttpResult()).WithName("UpdateCliente");

        return app;
    }
}
