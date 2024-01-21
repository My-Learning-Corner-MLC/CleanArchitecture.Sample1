using MediatR;
using Microsoft.EntityFrameworkCore;
using Sample1.Application.Common.Constants;
using Sample1.Application.Common.Exceptions;
using Sample1.Application.Common.Interfaces;

namespace Sample1.Application.ProductItems.Commands.UpdateProductItem;

public record UpdateProductItemCommand : IRequest<int>
{
    public int Id {get; init;}
    public string? Name { get; init; }
    public string? Description { get; init; }
    public decimal Price { get; init; }
    public string? PictureFileName { get; init; }
    public string? PictureUri { get; init; }
    public int ProductTypeId { get; init; }
    public int ProductBrandId { get; init; }
}

public class UpdateProductItemCommandHandler : IRequestHandler<UpdateProductItemCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductItemCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(UpdateProductItemCommand request, CancellationToken cancellationToken)
    {
        var productItem = await _unitOfWork.Products.GetById(request.Id, cancellationToken);

        if (productItem is null) throw new NotFoundException(
            errorMessage: ExceptionConst.ErrorMessage.RESOURCE_NOT_FOUND, 
            errorDescription: ExceptionConst.ErrorDescription.COULD_NOT_FOUND_ITEM_WITH_ID + request.Id
        );

        var sameNameProduct = await _unitOfWork.Products.GetByName(request.Name ?? string.Empty, cancellationToken);
        
        if (sameNameProduct is not null && sameNameProduct.Id != request.Id) throw new ValidationException(
            errorDescription: ValidationConst.ErrorMessage.PRODUCT_NAME_ALREADY_EXISTS
        );

        _unitOfWork.Products.Update(productItem);

        productItem.Name = request.Name;
        productItem.Description = request.Description;
        productItem.Price = request.Price;
        productItem.PictureFileName = request.PictureFileName;
        productItem.PictureUri = request.PictureUri;
        productItem.ProductTypeId = request.ProductTypeId;
        productItem.ProductBrandId = request.ProductBrandId;
        productItem.LastModified = DateTime.Now;

        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return productItem.Id;
    }
}