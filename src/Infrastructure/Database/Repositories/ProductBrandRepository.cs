using Microsoft.Extensions.Logging;
using Sample1.Application.Common.Interfaces;
using Sample1.Domain.Entities;

namespace Sample1.Infrastructure.Database.Repositories;

public class ProductBrandRepository : GenericRepository<ProductBrand>, IProductBrandRepository
{
    public ProductBrandRepository(ApplicationDbContext context, ILogger<UnitOfWork> logger)
        : base(context, logger) { }

    public async Task<ProductBrand?> GetById(int brandId, bool trackingChanges = false, CancellationToken cancellationToken = default)
    {
        return await GetBy(
            predicateExpression: p => p.Id == brandId, 
            trackingChanges: trackingChanges,
            cancellationToken: cancellationToken
        );
    }
}