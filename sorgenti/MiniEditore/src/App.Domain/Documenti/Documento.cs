using App.Domain.Common;

namespace App.Domain.Documenti;

public class Documento : Entity
{
    private readonly List<DocumentoRiga> _righe = new();

    private Documento()
    {
        Numero = string.Empty;
    }

    public Documento(string numero, DateOnly data, int clienteId, TipoDocumento tipoDocumento)
    {
        if (string.IsNullOrWhiteSpace(numero)) throw new DomainException("Numero obbligatorio");
        if (clienteId <= 0) throw new DomainException("ClienteId deve essere > 0");

        Numero = numero.Trim();
        Data = data;
        ClienteId = clienteId;
        TipoDocumento = tipoDocumento;
        Stato = StatoDocumento.Bozza;
        RecalcolaTotali();
    }

    public string Numero { get; private set; }
    public DateOnly Data { get; private set; }
    public int ClienteId { get; private set; }
    public TipoDocumento TipoDocumento { get; private set; }
    public StatoDocumento Stato { get; private set; }
    public decimal TotaleImponibile { get; private set; }
    public decimal TotaleSconto { get; private set; }
    public decimal TotaleDocumento { get; private set; }
    public IReadOnlyCollection<DocumentoRiga> Righe => _righe.AsReadOnly();

    public void AggiungiRiga(DocumentoRiga riga)
    {
        EnsureBozza();
        if (riga == null) throw new DomainException("Riga nulla");
        _righe.Add(riga);
        RecalcolaTotali();
    }

    public void ModificaRiga(int indiceRiga, int libroId, string descrizione, int quantita, decimal prezzoUnitario, decimal scontoPercentuale)
    {
        EnsureBozza();
        if (indiceRiga < 0 || indiceRiga >= _righe.Count) throw new DomainException("Indice riga fuori range");
        _righe[indiceRiga].ImpostaDati(libroId, descrizione, quantita, prezzoUnitario, scontoPercentuale);
        RecalcolaTotali();
    }

    public void RimuoviRiga(int indiceRiga)
    {
        EnsureBozza();
        if (indiceRiga < 0 || indiceRiga >= _righe.Count) throw new DomainException("Indice riga fuori range");
        _righe.RemoveAt(indiceRiga);
        RecalcolaTotali();
    }

    public void Conferma()
    {
        if (Stato != StatoDocumento.Bozza) throw new DomainException("Solo documenti in bozza possono essere confermati");
        if (!_righe.Any()) throw new DomainException("Documento senza righe non può essere confermato");
        Stato = StatoDocumento.Confermato;
    }

    public void Annulla()
    {
        Stato = StatoDocumento.Annullato;
    }

    private void EnsureBozza()
    {
        if (Stato != StatoDocumento.Bozza) throw new DomainException("Solo documenti in bozza possono essere modificati");
    }

    private void RecalcolaTotali()
    {
        TotaleImponibile = _righe.Sum(r => r.TotaleLordo);
        TotaleDocumento = _righe.Sum(r => r.TotaleRiga);
        TotaleSconto = TotaleImponibile - TotaleDocumento;
    }
}
