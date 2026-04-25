using App.Domain.Common;

namespace App.Domain.Catalogo;

public class Collana : Entity
{
    private readonly List<Libro> _libri = new();

    private Collana()
    {
        Nome = string.Empty;
    }

    public Collana(string nome, string? descrizione = null)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new DomainException("Nome collana obbligatorio");

        Nome = nome.Trim();
        Descrizione = string.IsNullOrWhiteSpace(descrizione) ? null : descrizione.Trim();
        Attiva = true;
    }

    public string Nome { get; private set; }
    public string? Descrizione { get; private set; }
    public bool Attiva { get; private set; }
    public IReadOnlyCollection<Libro> Libri => _libri.AsReadOnly();

    public void ImpostaDati(string nome, string? descrizione)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new DomainException("Nome collana obbligatorio");

        Nome = nome.Trim();
        Descrizione = string.IsNullOrWhiteSpace(descrizione) ? null : descrizione.Trim();
    }

    public void AttivaCollana() => Attiva = true;
    public void DisattivaCollana() => Attiva = false;
}
