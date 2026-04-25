using Microsoft.EntityFrameworkCore;
using App.Application.Abstractions;
using App.Domain.Catalogo;
using App.Domain.Clienti;
using App.Domain.Documenti;

namespace App.Infrastructure.Data;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Libro> Libri { get; set; } = null!;
    public DbSet<Autore> Autori { get; set; } = null!;
    public DbSet<Collana> Collane { get; set; } = null!;
    public DbSet<LibroAutore> LibriAutori { get; set; } = null!;
    public DbSet<Cliente> Clienti { get; set; } = null!;
    public DbSet<Documento> Documenti { get; set; } = null!;
    public DbSet<DocumentoRiga> DocumentoRighe { get; set; } = null!;

    public new DbSet<T> Set<T>() where T : class => base.Set<T>();

    public new Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => base.SaveChangesAsync(cancellationToken);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
