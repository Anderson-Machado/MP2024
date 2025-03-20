using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MP.Core.Entities;

namespace MP.Infrastructure.Data.Mappings
{
    public class FotoPessoaMap : IEntityTypeConfiguration<FotoPessoa>
    {
        public void Configure(EntityTypeBuilder<FotoPessoa> builder)
        {
            builder.ToTable("PESSOA_FOTO");

    
            builder.Property(c => c.Id)
                .HasColumnName("CD_PESSOA");

            builder.Property(c => c.Imagem)
                .HasColumnName("BN_FOTO");

            builder.Ignore(c => c.Notifications);
            builder.Ignore(c => c.IsValid);
        }
    }
}
