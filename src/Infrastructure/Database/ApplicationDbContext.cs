using System.Reflection;
using Sample1.Application.Common.Interfaces;
using Sample1.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Sample1.Infrastructure.Database;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<ProductItem> ProductItems => Set<ProductItem>();

    public DbSet<ProductBrand> ProductBrands => Set<ProductBrand>();

    public DbSet<ProductType> ProductTypes => Set<ProductType>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
