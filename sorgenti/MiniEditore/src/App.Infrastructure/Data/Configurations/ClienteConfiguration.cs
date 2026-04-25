using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using App.Domain.Clienti;

namespace App.Infrastructure.Data.Configurations;

internal class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clienti");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.RagioneSociale).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Email).HasMaxLength(200);
        builder.Property(x => x.Telefono).HasMaxLength(50);
        builder.Property(x => x.PartitaIva).HasMaxLength(30);
        builder.Property(x => x.CodiceFiscale).HasMaxLength(30);
        builder.Property(x => x.Attivo).IsRequired();

        builder.HasIndex(x => x.RagioneSociale);
    }
}
