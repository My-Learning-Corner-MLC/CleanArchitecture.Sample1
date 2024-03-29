using MediatR;
using Microsoft.EntityFrameworkCore;
using Sample1.Application.Common.Constants;
using Sample1.Application.Common.Exceptions;
using Sample1.Application.Common.Interfaces;

namespace Sample1.Application.ProductItems.Commands.DeleteProductItem;

public record DeleteProductItemCommand : IRequest
{
    public int Id { get; init; }
}

public class DeleteProductItemCommandHandler : IRequestHandler<DeleteProductItemCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductItemCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteProductItemCommand request, CancellationToken cancellationToken)
    {
        var productItem = await _unitOfWork.Products.GetById(request.Id, trackingChanges: true, cancellationToken);
        if (productItem is null) throw new NotFoundException(
            errorMessage: ExceptionConst.ErrorMessages.RESOURCE_NOT_FOUND, 
            errorDescription: ExceptionConst.ErrorDescriptions.COULD_NOT_FOUND_ITEM_WITH_ID(request.Id)
        );

        _unitOfWork.Products.Delete(productItem);

        await _unitOfWork.SaveChangeAsync(cancellationToken);
    }
}