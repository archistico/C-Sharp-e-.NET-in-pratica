using App.Domain.Common;

namespace App.Domain.Clienti;

public class Cliente : Entity
{
    private Cliente()
    {
        RagioneSociale = string.Empty;
    }

    public Cliente(string ragioneSociale, string? email = null, string? telefono = null, string? partitaIva = null, string? codiceFiscale = null)
    {
        ImpostaDati(ragioneSociale, email, telefono, partitaIva, codiceFiscale);
        Attivo = true;
    }

    public string RagioneSociale { get; private set; }
    public string? Email { get; private set; }
    public string? Telefono { get; private set; }
    public string? PartitaIva { get; private set; }
    public string? CodiceFiscale { get; private set; }
    public bool Attivo { get; private set; }

    public void ImpostaDati(string ragioneSociale, string? email, string? telefono, string? partitaIva, string? codiceFiscale)
    {
        if (string.IsNullOrWhiteSpace(ragioneSociale))
            throw new DomainException("Ragione sociale obbligatoria");

        RagioneSociale = ragioneSociale.Trim();
        Email = string.IsNullOrWhiteSpace(email) ? null : email.Trim();
        Telefono = string.IsNullOrWhiteSpace(telefono) ? null : telefono.Trim();
        PartitaIva = string.IsNullOrWhiteSpace(partitaIva) ? null : partitaIva.Trim();
        CodiceFiscale = string.IsNullOrWhiteSpace(codiceFiscale) ? null : codiceFiscale.Trim();
    }

    public void AttivaCliente() => Attivo = true;
    public void DisattivaCliente() => Attivo = false;
}
