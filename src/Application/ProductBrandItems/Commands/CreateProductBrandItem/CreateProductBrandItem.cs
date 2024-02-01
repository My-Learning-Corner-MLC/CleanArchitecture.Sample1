using MediatR;
using Sample1.Application.Common.Constants;
using Sample1.Application.Common.Exceptions;
using Sample1.Application.Common.Interfaces;
using Sample1.Domain.Entities;

namespace Sample1.Application.ProductBrandItems.Commands.CreateProductBrandItem;

public record CreateProductBrandItemCommand : IRequest<int>
{
    public string Name { get; init; } = string.Empty;
}

public class CreateProductBrandItemCommandHandler : IRequestHandler<CreateProductBrandItemCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductBrandItemCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateProductBrandItemCommand request, CancellationToken cancellationToken)
    {
        var isExistsBrandName = await _unitOfWork.ProductBrands.GetBy(b => b.Brand == request.Name, cancellationToken: cancellationToken);
        if (isExistsBrandName is not null) 
            throw new ValidationException(
                errorDescription: BrandConst.ErrorMessages.BRAND_NAME_ALREADY_EXISTS
            );

        var entity = new ProductBrand
        {
            Brand = request.Name,
        };

        _unitOfWork.ProductBrands.Add(entity);

        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return entity.Id;
    }
}