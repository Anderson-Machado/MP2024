using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MP.Core.Entities;

namespace MP.Infrastructure.Data.Mappings
{
    public class SituacaoPessoaMultiplaMap : IEntityTypeConfiguration<SituacaoPessoaMultipla>
    {
        public void Configure(EntityTypeBuilder<SituacaoPessoaMultipla> builder)
        {
            builder.ToTable("SITUACAO_PESSOA_MULTIPLA");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasColumnName("CD_SITUACAO_PESSOA_MULTIPLA");

            builder.Property(c => c.DatePeriodoFinal)
                .HasColumnName("DT_PERIODO_FINAL");


            builder.Property(c => c.CodPessoa)
                .HasColumnName("CD_PESSOA");

            builder.Ignore(c => c.Notifications);
            builder.Ignore(c => c.IsValid);

        }
    }
}