using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MP.Core.Entities;

namespace MP.Infrastructure.Data.Mappings
{
    public class PessoaMap : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.ToTable("PESSOA");
            builder.HasOne(p => p.FotoPessoa).WithOne().HasForeignKey<FotoPessoa>(f => f.Id);
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasColumnName("CD_PESSOA");

            builder.Property(c => c.CodSituacaoPessoa)
                .HasColumnName("CD_SITUACAO_PESSOA");


            builder.Property(c => c.Matricula)
                .HasColumnName("NU_MATRICULA");

            builder.Property(c => c.NomePessoa)
    .HasColumnName("NM_PESSOA");

            

            builder.Ignore(c => c.Notifications);
            builder.Ignore(c => c.IsValid);
            builder.Ignore(c => c.SituacaoPessoa);
            builder.Ignore(c => c.FotoPessoa);
        }

    }
}