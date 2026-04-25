using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using App.Domain.Catalogo;

namespace App.Infrastructure.Data.Configurations;

internal class LibroAutoreConfiguration : IEntityTypeConfiguration<LibroAutore>
{
    public void Configure(EntityTypeBuilder<LibroAutore> builder)
    {
        builder.ToTable("LibriAutori");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.LibroId).IsRequired();
        builder.Property(x => x.AutoreId).IsRequired();

        builder.HasIndex(x => new { x.LibroId, x.AutoreId }).IsUnique();

        builder.HasOne(la => la.Libro)
            .WithMany(l => l.Autori)
            .HasForeignKey(la => la.LibroId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(la => la.Autore)
            .WithMany(a => a.Libri)
            .HasForeignKey(la => la.AutoreId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
