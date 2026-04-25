using App.Domain.Common;

namespace App.Domain.Catalogo;

public class LibroAutore : Entity
{
    private LibroAutore()
    {
    }

    public LibroAutore(int libroId, int autoreId)
    {
        if (autoreId <= 0) throw new DomainException("AutoreId deve essere > 0");

        // Quando il libro non è ancora stato salvato, Id può essere 0.
        // EF Core valorizzerà LibroId tramite la relazione quando salva il nuovo libro.
        if (libroId < 0) throw new DomainException("LibroId non può essere negativo");

        LibroId = libroId;
        AutoreId = autoreId;
    }

    public int LibroId { get; private set; }
    public Libro? Libro { get; private set; }
    public int AutoreId { get; private set; }
    public Autore? Autore { get; private set; }
}
