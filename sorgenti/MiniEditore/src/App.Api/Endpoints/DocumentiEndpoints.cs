using App.Application.Documenti;
using App.Api.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace App.Api.Endpoints;

public static class DocumentiEndpoints
{
    public static IEndpointRouteBuilder MapDocumentiEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/documenti").WithTags("Documenti");

        group.MapGet("", async (IDocumentoQueries q) => await q.GetDocumentiAsync()).WithName("GetDocumenti");

        group.MapGet("/{id:int}", async (int id, IDocumentoQueries q) =>
        {
            var dto = await q.GetDocumentoDetailAsync(id);
            return dto == null ? Results.NotFound() : Results.Ok(dto);
        }).WithName("GetDocumento");

        group.MapPost("", async (CreateDocumentoRequest req, IDocumentoRepository repo) =>
        {
            var numero = $"DOC-{DateTime.UtcNow.Ticks}";
            var doc = new App.Domain.Documenti.Documento(numero, req.Data, req.ClienteId, req.TipoDocumento);
            foreach (var r in req.Righe)
            {
                var prezzo = r.PrezzoUnitario ?? 0m;
                var descr = $"Riga libro {r.LibroId}";
                doc.AggiungiRiga(new App.Domain.Documenti.DocumentoRiga(r.LibroId, descr, r.Quantita, prezzo, r.ScontoPercentuale));
            }
            await repo.AddAsync(doc);
            await repo.SaveChangesAsync();
            return Results.Created($"/api/documenti/{doc.Id}", new { id = doc.Id });
        }).WithName("CreateDocumento");

        group.MapPost("/{id:int}/conferma", async (int id, IDocumentoRepository repo) =>
        {
            var d = await repo.GetByIdAsync(id);
            if (d == null) return Results.NotFound();
            try
            {
                d.Conferma();
                await repo.SaveChangesAsync();
                return Results.NoContent();
            }
            catch (App.Domain.Common.DomainException ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
        }).WithName("ConfermaDocumento");

        group.MapPost("/{id:int}/annulla", async (int id, IDocumentoRepository repo) =>
        {
            var d = await repo.GetByIdAsync(id);
            if (d == null) return Results.NotFound();
            d.Annulla();
            await repo.SaveChangesAsync();
            return Results.NoContent();
        }).WithName("AnnullaDocumento");

        return app;
    }
}
