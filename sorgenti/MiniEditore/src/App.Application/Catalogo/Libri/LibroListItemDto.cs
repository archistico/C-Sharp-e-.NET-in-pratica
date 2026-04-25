namespace App.Application.Catalogo.Libri;

public sealed record LibroListItemDto(int Id, string Titolo, string? Isbn, decimal Prezzo, bool Attivo);
