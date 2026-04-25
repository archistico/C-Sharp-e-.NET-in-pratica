using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using App.Domain.Catalogo;

namespace App.Infrastructure.Data.Configurations;

internal class AutoreConfiguration : IEntityTypeConfiguration<Autore>
{
    public void Configure(EntityTypeBuilder<Autore> builder)
    {
        builder.ToTable("Autori");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Nome).HasMaxLength(100);
        builder.Property(x => x.Cognome).HasMaxLength(100);
        builder.Property(x => x.Email).HasMaxLength(200);
        builder.Property(x => x.Note).HasMaxLength(1000);

        builder.HasMany(a => a.Libri)
            .WithOne(la => la.Autore)
            .HasForeignKey(la => la.AutoreId);

        builder.Navigation(e => e.Libri).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
