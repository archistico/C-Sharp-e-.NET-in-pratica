namespace App.Application.Dashboard;

public sealed record DashboardSummaryDto(
    int NumeroLibriAttivi,
    int NumeroAutori,
    int NumeroClienti,
    int NumeroDocumentiMese,
    decimal TotaleDocumentiMese,
    IReadOnlyList<UltimoDocumentoDto> UltimiDocumenti,
    IReadOnlyList<LibroPiuVendutoDto> LibriPiuVenduti);

public sealed record UltimoDocumentoDto(
    int Id,
    string Numero,
    DateOnly Data,
    string Cliente,
    decimal TotaleDocumento);

public sealed record LibroPiuVendutoDto(
    int LibroId,
    string Titolo,
    int QuantitaVenduta,
    decimal TotaleVenduto);
