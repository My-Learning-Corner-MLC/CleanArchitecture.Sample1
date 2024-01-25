using Microsoft.Extensions.Logging;
using Sample1.Application.Common.Interfaces;
using Sample1.Domain.Entities;

namespace Sample1.Infrastructure.Database.Repositories;

public class ProductTypeRepository : GenericRepository<ProductType>, IProductTypeRepository
{
    public ProductTypeRepository(ApplicationDbContext context, ILogger<UnitOfWork> logger)
        : base(context, logger) { }

    public async Task<ProductType?> GetById(int typeId, CancellationToken cancellationToken)
    {
        return await GetBy(
            predicateExpression: p => p.Id == typeId, 
            cancellationToken: cancellationToken
        );
    }
}