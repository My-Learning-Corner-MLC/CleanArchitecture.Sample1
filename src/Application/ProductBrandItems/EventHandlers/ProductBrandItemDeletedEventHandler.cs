using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Sample1.Application.Common.Interfaces;
using Sample1.Domain.Events;
using Sample1.Domain.IntegrationEvents;

namespace Sample1.Application.ProductBrandItems.EventHandlers;

public class ProductBrandItemDeletedEventHandler : INotificationHandler<ProductBrandItemDeletedEvent>
{
    private readonly ILogger<ProductBrandItemDeletedEvent> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublishEndpoint _publishEndpoint;

    public ProductBrandItemDeletedEventHandler(ILogger<ProductBrandItemDeletedEvent> logger, IUnitOfWork unitOfWork, IPublishEndpoint publishEndpoint)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Handle(ProductBrandItemDeletedEvent @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event Triggered: {DomainEvent}", @event.GetType().Name);

        var products = await _unitOfWork.Products.GetAll(new()
        {
            FilterExpression = (p) => p.ProductBrandId == @event.Item.Id,
            TrackingChanges = true,
            Page = 0,
            Size = 0
        });

        _unitOfWork.Products.DeleteRange(products);

        await _unitOfWork.SaveChangeAsync(cancellationToken);

        var eventBusMessage = new ProductBrandItemDeletedIntegrationEvent(@event.Item);
        _logger.LogInformation("Event {DomainEvent} publishing a message with Id: {EventId}", @event.GetType().Name, eventBusMessage.EventId);
        
        await _publishEndpoint.Publish(eventBusMessage, cancellationToken);
    }
}