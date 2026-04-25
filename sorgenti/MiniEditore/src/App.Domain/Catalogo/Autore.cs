using App.Domain.Common;

namespace App.Domain.Catalogo;

public class Autore : Entity
{
    private readonly List<LibroAutore> _libri = new();

    private Autore()
    {
        Nome = string.Empty;
        Cognome = string.Empty;
    }

    public Autore(string? nome, string? cognome, string? email = null, string? note = null)
    {
        if (string.IsNullOrWhiteSpace(nome) && string.IsNullOrWhiteSpace(cognome))
            throw new DomainException("Nome o cognome obbligatorio");

        Nome = (nome ?? string.Empty).Trim();
        Cognome = (cognome ?? string.Empty).Trim();
        Email = string.IsNullOrWhiteSpace(email) ? null : email.Trim();
        Note = string.IsNullOrWhiteSpace(note) ? null : note.Trim();
    }

    public string Nome { get; private set; }
    public string Cognome { get; private set; }
    public string NomeVisualizzato => string.Join(' ', new[] { Nome, Cognome }.Where(s => !string.IsNullOrWhiteSpace(s))).Trim();
    public string? Email { get; private set; }
    public string? Note { get; private set; }
    public IReadOnlyCollection<LibroAutore> Libri => _libri.AsReadOnly();

    public void ImpostaDati(string? nome, string? cognome, string? email, string? note)
    {
        if (string.IsNullOrWhiteSpace(nome) && string.IsNullOrWhiteSpace(cognome))
            throw new DomainException("Nome o cognome obbligatorio");

        Nome = (nome ?? string.Empty).Trim();
        Cognome = (cognome ?? string.Empty).Trim();
        Email = string.IsNullOrWhiteSpace(email) ? null : email.Trim();
        Note = string.IsNullOrWhiteSpace(note) ? null : note.Trim();
    }
}
