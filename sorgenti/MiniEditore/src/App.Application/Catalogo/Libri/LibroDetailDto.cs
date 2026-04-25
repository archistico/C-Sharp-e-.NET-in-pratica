using System.Collections.Generic;

namespace App.Application.Catalogo.Libri;

public sealed record LibroDetailDto(
    int Id,
    string Titolo,
    string? Isbn,
    decimal Prezzo,
    DateOnly? DataPubblicazione,
    string? Collana,
    IReadOnlyList<string> Autori,
    bool Attivo);
