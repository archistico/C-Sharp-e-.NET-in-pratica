using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using App.Domain.Documenti;

namespace App.Infrastructure.Data.Configurations;

internal class DocumentoConfiguration : IEntityTypeConfiguration<Documento>
{
    public void Configure(EntityTypeBuilder<Documento> builder)
    {
        builder.ToTable("Documenti");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Numero).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Data).IsRequired();
        builder.Property(x => x.ClienteId).IsRequired();
        builder.Property(x => x.TipoDocumento).IsRequired();
        builder.Property(x => x.Stato).IsRequired();

        builder.Property<decimal>("TotaleImponibile").HasPrecision(18, 2);
        builder.Property<decimal>("TotaleSconto").HasPrecision(18, 2);
        builder.Property<decimal>("TotaleDocumento").HasPrecision(18, 2);

        builder.HasIndex(x => x.Numero).IsUnique();
        builder.HasIndex(x => x.Data);
        builder.HasIndex(x => x.ClienteId);

        builder.HasMany(x => x.Righe)
            .WithOne()
            .HasForeignKey("DocumentoId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(e => e.Righe).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
