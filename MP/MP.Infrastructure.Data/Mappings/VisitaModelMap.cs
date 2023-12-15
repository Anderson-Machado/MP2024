using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MP.Core.Entities;

namespace MP.Infrastructure.Data.Mappings
{
    public class VisitaModelMap : IEntityTypeConfiguration<Visita>
    {
        public void Configure(EntityTypeBuilder<Visita> builder)
        {
            builder.ToTable("VISITA");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasColumnName("CD_VISITA");

            builder.Property(c => c.CodVisitante)
                .HasColumnName("CD_VISITANTE");

            builder.Property(c => c.DataBaixaCredencial)
                .HasColumnName("DT_BAIXA_CREDENCIAL");

            builder.Property(c => c.Observacao)
                .HasColumnName("DS_OBSERVACAO");


            builder.Ignore(c => c.Notifications);
            builder.Ignore(c => c.IsValid);
        }
    }
}
