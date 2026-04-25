using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using App.Domain.Catalogo;

namespace App.Infrastructure.Data.Configurations;

internal class CollanaConfiguration : IEntityTypeConfiguration<Collana>
{
    public void Configure(EntityTypeBuilder<Collana> builder)
    {
        builder.ToTable("Collane");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Nome).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Descrizione).HasMaxLength(1000);
        builder.Property(x => x.Attiva).IsRequired();

        builder.HasMany(c => c.Libri)
            .WithOne(l => l.Collana)
            .HasForeignKey("CollanaId");

        builder.Navigation(e => e.Libri).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
