using Sample1.Domain.Entities;

namespace Sample1.Application.Common.Interfaces;

public interface IProductRepository : IGenericRepository<ProductItem>
{
    Task <ProductItem?> GetById(int productId, CancellationToken cancellationToken);
    Task <ProductItem?> GetByName(string productName, CancellationToken cancellationToken);
}