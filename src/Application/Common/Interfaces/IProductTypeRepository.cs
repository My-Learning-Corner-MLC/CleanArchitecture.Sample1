using Sample1.Domain.Entities;

namespace Sample1.Application.Common.Interfaces;

public interface IProductTypeRepository : IGenericRepository<ProductType>
{
    Task <ProductType?> GetById(int typeId, CancellationToken cancellationToken);
}