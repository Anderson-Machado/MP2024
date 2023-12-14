using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MP.Core.Entities;

namespace MP.Infrastructure.Data.Mappings
{
    public class ExampleMap : IEntityTypeConfiguration<Example>
    {
        public void Configure(EntityTypeBuilder<Example> builder)
        {
            builder.ToTable("Examples");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasColumnName("ExampleId");

            builder.Property(c => c.Name)
                .HasColumnName("Name")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.ModifiedAt)
                .HasColumnName("ModifiedAt")
                .IsRequired();

            builder.Property(c => c.CreatedAt)
                .HasColumnName("CreatedAt")
                .IsRequired();

            builder.Ignore(c => c.Notifications);
            builder.Ignore(c => c.IsValid);
        }
    }
}