using MediatR;
using Microsoft.Extensions.Logging;
using Sample1.Application.Common.Interfaces;
using Sample1.Domain.Events;

namespace Sample1.Application.ProductBrandItems.EventHandlers;

public class ProductBrandItemDeletedEventHandler : INotificationHandler<ProductBrandItemDeletedEvent>
{
    private readonly ILogger<ProductBrandItemDeletedEvent> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public ProductBrandItemDeletedEventHandler(ILogger<ProductBrandItemDeletedEvent> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ProductBrandItemDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event Triggered: {DomainEvent}", notification.GetType().Name);

        var products = await _unitOfWork.Products.GetAll(new()
        {
            FilterExpression = (p) => p.ProductBrandId == notification.Item.Id,
            TrackingChanges = true,
            Page = 0,
            Size = 0
        });

        _unitOfWork.Products.DeleteRange(products);

        await _unitOfWork.SaveChangeAsync(cancellationToken);
    }
}