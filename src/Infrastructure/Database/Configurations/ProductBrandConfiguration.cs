using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample1.Domain.Entities;

namespace Sample1.Infrastructure.Database.Configurations;

class ProductBrandConfiguration : IEntityTypeConfiguration<ProductBrand>
{
    public void Configure(EntityTypeBuilder<ProductBrand> builder)
    {
        builder.ToTable("ProductBrand");

        builder.Property(pb => pb.Brand)
            .HasMaxLength(100);
    }
}
