using App.Domain.Common;

namespace App.Domain.Catalogo;

public class Libro : Entity
{
    private readonly List<LibroAutore> _autori = new();

    private Libro()
    {
        Titolo = string.Empty;
    }

    public Libro(string titolo, string? isbn, decimal prezzo, DateOnly? dataPubblicazione = null, int? collanaId = null)
    {
        ImpostaDati(titolo, isbn, prezzo, dataPubblicazione, collanaId);
        Attivo = true;
    }

    public string Titolo { get; private set; }
    public string? Isbn { get; private set; }
    public decimal Prezzo { get; private set; }
    public DateOnly? DataPubblicazione { get; private set; }
    public bool Attivo { get; private set; }
    public int? CollanaId { get; private set; }
    public Collana? Collana { get; private set; }
    public IReadOnlyCollection<LibroAutore> Autori => _autori.AsReadOnly();

    public void ImpostaDati(string titolo, string? isbn, decimal prezzo, DateOnly? dataPubblicazione, int? collanaId)
    {
        if (string.IsNullOrWhiteSpace(titolo))
            throw new DomainException("Titolo obbligatorio");
        if (prezzo < 0)
            throw new DomainException("Prezzo non può essere negativo");

        Titolo = titolo.Trim();
        Isbn = string.IsNullOrWhiteSpace(isbn) ? null : isbn.Trim();
        Prezzo = prezzo;
        DataPubblicazione = dataPubblicazione;
        CollanaId = collanaId;
    }

    public void Attiva() => Attivo = true;
    public void Disattiva() => Attivo = false;

    public void ImpostaAutori(IEnumerable<int> autoriIds)
    {
        if (autoriIds is null)
            throw new DomainException("Autori obbligatori");

        List<int> ids = autoriIds.ToList();
        if (ids.Any(id => id <= 0))
            throw new DomainException("Id autore deve essere > 0");

        _autori.Clear();
        foreach (int autoreId in ids.Distinct())
        {
            _autori.Add(new LibroAutore(Id, autoreId));
        }
    }
}
