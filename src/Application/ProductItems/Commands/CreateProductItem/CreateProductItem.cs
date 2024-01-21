using MediatR;
using Sample1.Application.Common.Constants;
using Sample1.Application.Common.Exceptions;
using Sample1.Application.Common.Interfaces;
using Sample1.Domain.Entities;

namespace Sample1.Application.ProductItems.Commands.CreateProductItem;

public record CreateProductItemCommand : IRequest<int>
{
    public string? Name { get; init; }
    public string? Description { get; init; }
    public decimal Price { get; init; }
    public string? PictureFileName { get; init; }
    public string? PictureUri { get; init; }
    public int ProductTypeId { get; init; }
    public int ProductBrandId { get; init; }
}

public class CreateProductItemCommandHandler : IRequestHandler<CreateProductItemCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductItemCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateProductItemCommand request, CancellationToken cancellationToken)
    {
        var sameNameProduct = await _unitOfWork.Products.GetByName(request.Name ?? string.Empty, cancellationToken);
        
        if (sameNameProduct is not null) throw new ValidationException(
            errorDescription: ValidationConst.ErrorMessage.PRODUCT_NAME_ALREADY_EXISTS
        );
        
        var entity = new ProductItem
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            PictureFileName = request.PictureFileName,
            PictureUri = request.PictureUri,
            ProductTypeId = request.ProductTypeId,
            ProductBrandId = request.ProductBrandId,
            Created = DateTime.Now,
            LastModified = DateTime.Now
        };

        _unitOfWork.Products.Add(entity);

        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return entity.Id;
    }
}