using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample1.Domain.Entities;

namespace Sample1.Infrastructure.Database.Configurations;

class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
{
    public void Configure(EntityTypeBuilder<ProductType> builder)
    {
        builder.ToTable("ProductType");

        builder.Property(cb => cb.Type)
            .HasMaxLength(100);
    }
}
