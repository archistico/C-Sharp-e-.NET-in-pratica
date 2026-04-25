using System.Collections.Generic;

namespace App.Application.Catalogo.Libri;

public sealed record UpdateLibroRequest(
    string Titolo,
    string? Isbn,
    decimal Prezzo,
    DateOnly? DataPubblicazione,
    int? CollanaId,
    IReadOnlyList<int> AutoriIds,
    bool Attivo);
