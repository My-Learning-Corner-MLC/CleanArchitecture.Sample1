using FluentValidation;

namespace Sample1.Application.ProductBrandItems.Commands.CreateProductBrandItem;

public class CreateProductBrandItemCommandValidator : AbstractValidator<CreateProductBrandItemCommand>
{
    public CreateProductBrandItemCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotNull()
            .NotEmpty();
    }
}