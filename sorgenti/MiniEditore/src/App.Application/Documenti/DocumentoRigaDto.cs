namespace App.Application.Documenti;

public sealed record DocumentoRigaDto(int Id, int LibroId, string Descrizione, int Quantita, decimal PrezzoUnitario, decimal ScontoPercentuale, decimal TotaleRiga);
