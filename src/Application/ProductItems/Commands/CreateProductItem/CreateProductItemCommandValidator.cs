using FluentValidation;
using Sample1.Application.Common.Constants;

namespace Sample1.Application.ProductItems.Commands.CreateProductItem;

public class CreateTodoItemCommandValidator : AbstractValidator<CreateProductItemCommand>
{
    private readonly int _nameMaxLength = ValidationConst.Value.NAME_MAX_LENTGH;
    private readonly int _uriMaxLenght = ValidationConst.Value.URI_MAX_LENGTH;
    private readonly decimal _maxPrice = ValidationConst.Value.MAX_PRICE;
    private readonly decimal _minPrice = ValidationConst.Value.MIN_PRICE;

    public CreateTodoItemCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotNull()
            .MaximumLength(_nameMaxLength);

        RuleFor(p => p.Price)
            .NotNull()
            .GreaterThan(_minPrice)
                .WithMessage(ValidationConst.ErrorMessage.PRODUCT_PRICE_SHOULD_BE_GREATER_THAN + _minPrice)
            .LessThan(_maxPrice)
                .WithMessage(ValidationConst.ErrorMessage.PRODUCT_PRICE_SHOULD_BE_LESS_THAN + _maxPrice);

        RuleFor(p => p.PictureFileName)
            .MaximumLength(_nameMaxLength);

        RuleFor(p => p.PictureUri)
            .NotNull()
            .MaximumLength(_uriMaxLenght);
            
        RuleFor(p => p.ProductTypeId)
            .NotNull();

        RuleFor(p => p.ProductBrandId)
            .NotNull();
    }
}