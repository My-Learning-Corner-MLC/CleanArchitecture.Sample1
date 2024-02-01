using Sample1.Domain.Common;
using Sample1.Domain.Entities;

namespace Sample1.Domain.Events;

public class ProductBrandItemDeletedEvent : BaseEvent
{
    public ProductBrand Item { get; }

    public ProductBrandItemDeletedEvent(ProductBrand productBrand)
    {
        Item = productBrand;
    }
}