namespace App.Application.Report;

public sealed record ReportVenditeRequest(DateOnly DataDa, DateOnly DataA);

public sealed record VenditePerLibroDto(
    int LibroId,
    string Titolo,
    int QuantitaVenduta,
    decimal TotaleVenduto);

public sealed record VenditePerClienteDto(
    int ClienteId,
    string Cliente,
    decimal TotaleVenduto);

public sealed record RiepilogoMensileDto(
    int Anno,
    int Mese,
    decimal TotaleVenduto,
    int NumeroDocumenti);
