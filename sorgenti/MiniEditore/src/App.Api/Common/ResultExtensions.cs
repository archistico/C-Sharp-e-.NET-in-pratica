using System;
using App.Application.Common;
using Microsoft.AspNetCore.Http;

namespace App.Api.Common;

public static class ResultExtensions
{
    private static bool IsNotFound(string? error)
    {
        if (string.IsNullOrWhiteSpace(error)) return false;
        return error.Contains("non trovato", StringComparison.OrdinalIgnoreCase)
            || error.Contains("non esiste", StringComparison.OrdinalIgnoreCase)
            || error.Contains("inesistente", StringComparison.OrdinalIgnoreCase);
    }

    public static IResult ToHttpResult(this Result result)
    {
        if (result.IsSuccess) return Results.NoContent();

        if (IsNotFound(result.Error)) return Results.NotFound(new { error = result.Error });

        return Results.BadRequest(new { error = result.Error });
    }

    public static IResult ToHttpResult<T>(this Result<T> result)
    {
        if (result.IsSuccess) return Results.Ok(result.Value!);

        if (IsNotFound(result.Error)) return Results.NotFound(new { error = result.Error });

        return Results.BadRequest(new { error = result.Error });
    }

    public static IResult ToCreatedResult(this Result<int> result, string routePrefix)
    {
        if (result.IsSuccess)
        {
            var id = result.Value;
            return Results.Created($"{routePrefix}/{id}", new { id });
        }

        return Results.BadRequest(new { error = result.Error });
    }
}
