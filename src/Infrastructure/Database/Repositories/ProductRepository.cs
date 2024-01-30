using Microsoft.Extensions.Logging;
using Sample1.Application.Common.Interfaces;
using Sample1.Domain.Entities;

namespace Sample1.Infrastructure.Database.Repositories;

public class ProductRepository : GenericRepository<ProductItem>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context, ILogger<UnitOfWork> logger)
        : base(context, logger) { }

    public async Task<ProductItem?> GetById(int productId, bool trackingChanges = false,  CancellationToken cancellationToken = default)
    {
        return await GetBy(
            predicateExpression: p => p.Id == productId, 
            trackingChanges: trackingChanges,
            cancellationToken: cancellationToken
        );
    }

    public async Task<ProductItem?> GetByName(string productName, CancellationToken cancellationToken = default)
    {
        return await GetBy(
            predicateExpression: p => p.Name == productName && !p.IsDeleted,
            cancellationToken: cancellationToken
        );
    }
}