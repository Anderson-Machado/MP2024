using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MP.Core.Entities;

namespace MP.Infrastructure.Data.Mappings
{
    public class VisitanteMap : IEntityTypeConfiguration<Visitante>
    {
        public void Configure(EntityTypeBuilder<Visitante> builder)
        {
            builder.ToTable("VISITANTE");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasColumnName("CD_VISITANTE");

            builder.Property(c => c.Nome)
                .HasColumnName("NM_VISITANTE");


            builder.Property(c => c.Matricula)
                .HasColumnName("CD_PESSOA");

            builder.Ignore(c => c.Notifications);
            builder.Ignore(c => c.Matricula);
            builder.Ignore(c => c.VisiNumero);
            builder.Ignore(c => c.VitaNumero);
            builder.Ignore(c => c.Result);

            builder.Ignore(c => c.IsValid);
        }
    }
}
