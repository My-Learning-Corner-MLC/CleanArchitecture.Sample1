using FluentValidation;

namespace Sample1.Application.ProductBrandItems.Commands.UpdateProductBrandItem;

public class UpdateProductBrandItemCommandValidator : AbstractValidator<UpdateProductBrandItemCommand>
{
    public UpdateProductBrandItemCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotNull()
            .NotEmpty();
    }
}