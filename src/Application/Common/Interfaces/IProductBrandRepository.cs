using Sample1.Domain.Entities;

namespace Sample1.Application.Common.Interfaces;

public interface IProductBrandRepository : IGenericRepository<ProductBrand>
{
    Task <ProductBrand?> GetById(int brandId, CancellationToken cancellationToken);
}