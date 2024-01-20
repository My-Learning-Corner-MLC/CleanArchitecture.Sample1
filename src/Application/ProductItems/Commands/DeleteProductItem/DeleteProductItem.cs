using MediatR;
using Microsoft.EntityFrameworkCore;
using Sample1.Application.Common.Constants;
using Sample1.Application.Common.Exceptions;
using Sample1.Application.Common.Interfaces;

namespace Sample1.Application.ProductItems.Commands.DeleteProductItem;

public record DeleteProductItemCommand : IRequest
{
    public int Id { get; init; }
}

public class DeleteProductItemCommandHandler : IRequestHandler<DeleteProductItemCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteProductItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteProductItemCommand request, CancellationToken cancellationToken)
    {
        var productItem = await _context.ProductItems
            .Where(p => p.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (productItem is null) throw new NotFoundException(
            errorMessage: ExceptionConst.ErrorMessage.RESOURCE_NOT_FOUND, 
            errorDescription: ExceptionConst.ErrorDescription.COULD_NOT_FOUND_ITEM_WITH_ID + request.Id
        );

        _context.ProductItems.Remove(productItem);

        await _context.SaveChangesAsync(cancellationToken);
    }
}