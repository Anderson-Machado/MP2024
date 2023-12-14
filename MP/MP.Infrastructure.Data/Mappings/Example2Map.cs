using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MP.Core.Entities;

namespace MP.Infrastructure.Data.Mappings
{
    public class Example2Map : IEntityTypeConfiguration<Example2>
    {
        public void Configure(EntityTypeBuilder<Example2> builder)
        {
            builder.ToTable("Examples2");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasColumnName("Example2Id");

            builder.Property(c => c.Description)
                .HasColumnName("Description")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.ModifiedAt)
                .HasColumnName("ModifiedAt")
                .IsRequired();

            builder.Property(c => c.CreatedAt)
                .HasColumnName("CreatedAt")
                .IsRequired();

            builder.Property(c => c.DeletedAt)
                .HasColumnName("DeletedAt");

            builder.HasOne(s => s.Other)
               .WithMany()
               .HasForeignKey(s => s.OtherId)
               .IsRequired();

            builder.Ignore(c => c.Notifications);
            builder.Ignore(c => c.IsValid);

            builder.HasQueryFilter(c => !c.DeletedAt.HasValue);
        }
    }
}