using Sample1.Domain.Common;

namespace Sample1.Domain.Entities;

public class ProductType : BaseAuditableEntity
{
    public string Type { get; set; }
}
