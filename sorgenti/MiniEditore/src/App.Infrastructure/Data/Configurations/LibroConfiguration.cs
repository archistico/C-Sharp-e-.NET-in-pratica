using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using App.Domain.Catalogo;

namespace App.Infrastructure.Data.Configurations;

internal class LibroConfiguration : IEntityTypeConfiguration<Libro>
{
    public void Configure(EntityTypeBuilder<Libro> builder)
    {
        builder.ToTable("Libri");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Titolo).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Isbn).HasMaxLength(30);
        builder.Property(x => x.Prezzo).HasPrecision(18, 2);
        builder.Property(x => x.DataPubblicazione).IsRequired(false);
        builder.Property(x => x.Attivo).IsRequired();
        builder.Property<int?>("CollanaId");

        builder.HasIndex(x => x.Isbn);

        builder.HasOne(x => x.Collana)
            .WithMany(c => c.Libri)
            .HasForeignKey("CollanaId")
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(x => x.Autori)
            .WithOne(la => la.Libro)
            .HasForeignKey(la => la.LibroId);

        builder.Navigation(e => e.Autori).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
