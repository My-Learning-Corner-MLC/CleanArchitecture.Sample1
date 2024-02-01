using MediatR;
using Sample1.Application.Common.Constants;
using Sample1.Application.Common.Exceptions;
using Sample1.Application.Common.Interfaces;

namespace Sample1.Application.ProductBrandItems.Commands.UpdateProductBrandItem;

public record UpdateProductBrandItemCommand : IRequest<int>
{
    public int Id {get; init;}

    public string Name { get; init; } = string.Empty;
}

public class UpdateProductBrandItemCommandHandler : IRequestHandler<UpdateProductBrandItemCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductBrandItemCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(UpdateProductBrandItemCommand request, CancellationToken cancellationToken)
    {
        var brandItem = await _unitOfWork.ProductBrands.GetById(request.Id, trackingChanges: true, cancellationToken);
        if (brandItem is null) 
            throw new NotFoundException(
                errorMessage: ExceptionConst.ErrorMessages.RESOURCE_NOT_FOUND, 
                errorDescription: ExceptionConst.ErrorDescriptions.COULD_NOT_FOUND_ITEM_WITH_ID(request.Id)
            );

        var isExistsBrandName = await _unitOfWork.ProductBrands.GetBy(b => b.Brand == request.Name, cancellationToken: cancellationToken); 
        var alreadyHasThisBrandName = isExistsBrandName is not null && isExistsBrandName.Id != request.Id;
        if (alreadyHasThisBrandName) 
            throw new ValidationException(
                errorDescription: BrandConst.ErrorMessages.BRAND_NAME_ALREADY_EXISTS
            );

        brandItem.Brand = request.Name;

        _unitOfWork.ProductBrands.Update(brandItem);

        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return brandItem.Id;
    }
}