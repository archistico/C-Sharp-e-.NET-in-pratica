using App.Domain.Common;

namespace App.Domain.Documenti;

public class DocumentoRiga : Entity
{
    private DocumentoRiga()
    {
        Descrizione = string.Empty;
    }

    public DocumentoRiga(int libroId, string descrizione, int quantita, decimal prezzoUnitario, decimal scontoPercentuale)
    {
        ImpostaDati(libroId, descrizione, quantita, prezzoUnitario, scontoPercentuale);
    }

    public int DocumentoId { get; private set; }
    public int LibroId { get; private set; }
    public string Descrizione { get; private set; }
    public int Quantita { get; private set; }
    public decimal PrezzoUnitario { get; private set; }
    public decimal ScontoPercentuale { get; private set; }

    public decimal TotaleLordo => Quantita * PrezzoUnitario;
    public decimal TotaleRiga => Quantita * PrezzoUnitario * (1 - ScontoPercentuale / 100m);

    public void ImpostaDati(int libroId, string descrizione, int quantita, decimal prezzoUnitario, decimal scontoPercentuale)
    {
        if (libroId <= 0) throw new DomainException("LibroId deve essere > 0");
        if (string.IsNullOrWhiteSpace(descrizione)) throw new DomainException("Descrizione obbligatoria");
        if (quantita <= 0) throw new DomainException("Quantità deve essere > 0");
        if (prezzoUnitario < 0) throw new DomainException("Prezzo unitario non può essere negativo");
        if (scontoPercentuale < 0 || scontoPercentuale > 100) throw new DomainException("Sconto deve essere tra 0 e 100");

        LibroId = libroId;
        Descrizione = descrizione.Trim();
        Quantita = quantita;
        PrezzoUnitario = prezzoUnitario;
        ScontoPercentuale = scontoPercentuale;
    }
}
