using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Sample1.Application.Common.Interfaces;
using Sample1.Application.Common.Constants;

namespace Sample1.Application.ProductItems.Commands.CreateProductItem;

public class CreateTodoItemCommandValidator : AbstractValidator<CreateProductItemCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly int _nameMaxLength = ValidationConst.Value.NAME_MAX_LENTGH;
    private readonly int _uriMaxLenght = ValidationConst.Value.URI_MAX_LENGTH;
    private readonly decimal _maxPrice = ValidationConst.Value.MAX_PRICE;
    private readonly decimal _minPrice = ValidationConst.Value.MIN_PRICE;

    public CreateTodoItemCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(p => p.Name)
            .NotNull()
            .MaximumLength(_nameMaxLength)
            .MustAsync(BeUniqueName)
                .WithMessage(ValidationConst.ErrorMessage.PRODUCT_NAME_ALREADY_EXISTS)
                .WithErrorCode("Unique");

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

    public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        return await _context.ProductItems
            .AllAsync(p => p.Name != name, cancellationToken);
    }
}