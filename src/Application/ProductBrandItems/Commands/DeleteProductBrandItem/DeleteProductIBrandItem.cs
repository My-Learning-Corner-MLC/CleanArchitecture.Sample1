using MediatR;
using Sample1.Application.Common.Constants;
using Sample1.Application.Common.Exceptions;
using Sample1.Application.Common.Interfaces;
using Sample1.Domain.Events;

namespace Sample1.Application.ProductBrandItems.Commands.DeleteProductBrandItem;

public record DeleteProductIBrandItemCommand : IRequest
{
    public int Id { get; init; }
}

public class DeleteProductIBrandItemCommandHandler : IRequestHandler<DeleteProductIBrandItemCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductIBrandItemCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteProductIBrandItemCommand request, CancellationToken cancellationToken)
    {
        var brandItem = await _unitOfWork.ProductBrands.GetById(request.Id, trackingChanges: true, cancellationToken);
        if (brandItem is null) 
            throw new NotFoundException(
                errorMessage: ExceptionConst.ErrorMessages.RESOURCE_NOT_FOUND, 
                errorDescription: ExceptionConst.ErrorDescriptions.COULD_NOT_FOUND_ITEM_WITH_ID(request.Id)
            );

        // Trigger to delete related records
        brandItem.AddDomainEvent(new ProductBrandItemDeletedEvent(brandItem));

        _unitOfWork.ProductBrands.Delete(brandItem);
        
        await _unitOfWork.SaveChangeAsync(cancellationToken);
    }
}