using Microsoft.EntityFrameworkCore;
using App.Domain.Catalogo;
using App.Domain.Clienti;
using App.Domain.Documenti;

namespace App.Application.Abstractions;

public interface IAppDbContext
{
    DbSet<Libro> Libri { get; }
    DbSet<Autore> Autori { get; }
    DbSet<Collana> Collane { get; }
    DbSet<LibroAutore> LibriAutori { get; }
    DbSet<Cliente> Clienti { get; }
    DbSet<Documento> Documenti { get; }
    DbSet<DocumentoRiga> DocumentoRighe { get; }

    DbSet<T> Set<T>() where T : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
