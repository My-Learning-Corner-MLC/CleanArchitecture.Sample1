using FluentValidation;
using Sample1.Application.Common.Constants;

namespace Sample1.Application.ProductItems.Commands.CreateProductItem;

public class CreateTodoItemCommandValidator : AbstractValidator<CreateProductItemCommand>
{
    private readonly int _nameMaxLength = ProductConst.Rules.NAME_MAX_LENTGH;
    private readonly int _uriMaxLenght = ProductConst.Rules.URI_MAX_LENGTH;
    private readonly decimal _maxPrice = ProductConst.Rules.MAX_PRICE;
    private readonly decimal _minPrice = ProductConst.Rules.MIN_PRICE;

    public CreateTodoItemCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(_nameMaxLength);

        RuleFor(p => p.Price)
            .NotNull()
            .GreaterThan(_minPrice)
                .WithMessage(ProductConst.ErrorMessages.PRODUCT_PRICE_SHOULD_BE_GREATER_THAN(_minPrice))
            .LessThan(_maxPrice)
                .WithMessage(ProductConst.ErrorMessages.PRODUCT_PRICE_SHOULD_BE_LESS_THAN(_maxPrice));

        RuleFor(p => p.PictureFileName)
            .MaximumLength(_nameMaxLength);

        RuleFor(p => p.PictureUri)
            .NotNull()
            .NotEmpty()
            .MaximumLength(_uriMaxLenght);
            
        RuleFor(p => p.ProductTypeId)
            .NotNull();

        RuleFor(p => p.ProductBrandId)
            .NotNull();
    }
}