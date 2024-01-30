using FluentValidation;

namespace Sample1.Application.ProductItems.Commands.UpdateProductItem;

public class UpdateProductBrandItemCommandValidator : AbstractValidator<UpdateProductBrandItemCommand>
{
    public UpdateProductBrandItemCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotNull()
            .NotEmpty();
    }
}