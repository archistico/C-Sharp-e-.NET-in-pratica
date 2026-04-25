namespace App.Application.Documenti;

public sealed record CreateDocumentoRigaRequest(int LibroId, int Quantita, decimal? PrezzoUnitario, decimal ScontoPercentuale);
