using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using App.Domain.Documenti;

namespace App.Infrastructure.Data.Configurations;

internal class DocumentoRigaConfiguration : IEntityTypeConfiguration<DocumentoRiga>
{
    public void Configure(EntityTypeBuilder<DocumentoRiga> builder)
    {
        builder.ToTable("DocumentoRighe");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.DocumentoId).IsRequired();
        builder.Property(x => x.LibroId).IsRequired();
        builder.Property(x => x.Descrizione).IsRequired().HasMaxLength(300);
        builder.Property(x => x.Quantita).IsRequired();
        builder.Property(x => x.PrezzoUnitario).HasPrecision(18, 2);
        builder.Property(x => x.ScontoPercentuale).HasPrecision(5, 2);

        builder.Ignore(x => x.TotaleLordo);
        builder.Ignore(x => x.TotaleRiga);

        builder.HasIndex(x => x.DocumentoId);
        builder.HasIndex(x => x.LibroId);

        builder.HasOne<Documento>()
            .WithMany(d => d.Righe)
            .HasForeignKey(r => r.DocumentoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
