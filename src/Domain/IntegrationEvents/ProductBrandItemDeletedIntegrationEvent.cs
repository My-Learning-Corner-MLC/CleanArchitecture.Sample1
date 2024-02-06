using MassTransit;
using Sample1.Domain.Entities;

namespace Sample1.Domain.IntegrationEvents;

[EntityName("products.brand-deleted")]
public record ProductBrandItemDeletedIntegrationEvent(ProductBrand Item, string? EventId = null)
{
    public string EventId { get; init; } = EventId ?? Guid.NewGuid().ToString();
}