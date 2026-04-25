namespace App.Web.Models;

public record UiResult(bool Success, string? Error)
{
    public static UiResult Ok() => new(true, null);
    public static UiResult Fail(string error) => new(false, error);
}
