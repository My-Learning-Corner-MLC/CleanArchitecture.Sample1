using FluentValidation;

namespace Sample1.Application.ProductItems.Commands.CreateProductItem;

public class CreateProductBrandItemCommandValidator : AbstractValidator<CreateProductBrandItemCommand>
{
    public CreateProductBrandItemCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotNull()
            .NotEmpty();
    }
}