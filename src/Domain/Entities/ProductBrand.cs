using Sample1.Domain.Common;

namespace Sample1.Domain.Entities;

public class ProductBrand : BaseAuditableEntity
{
    public string Brand { get; set; }
}
