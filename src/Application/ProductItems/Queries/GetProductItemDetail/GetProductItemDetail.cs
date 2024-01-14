using AutoMapper;
using MediatR;
using Sample1.Application.Common.Exceptions;
using Sample1.Application.Common.Interfaces;
using Sample1.Application.Common.Mappings;

namespace Sample1.Application.ProductItems.Queries.GetProductItemDetail;

public record GetProductItemDetailQuery : IRequest<ProductItemDetailDto>
{
    public int Id { get; init; }
}

public class GetProductItemDetailQueryHandler : IRequestHandler<GetProductItemDetailQuery, ProductItemDetailDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProductItemDetailQueryHandler(IApplicationDbContext context, IMapper mapper)
    => (_context, _mapper) = (context, mapper);

    public async Task<ProductItemDetailDto> Handle(GetProductItemDetailQuery request, CancellationToken cancellationToken)
    {
        var productItem = await _context.ProductItems
            .Where(p => p.Id == request.Id)
            .GetItemAsync(cancellationToken);

        if (productItem is null) throw new NotFoundException(
            errorMessage: "Resource not found", 
            errorDescription: $"Could not found item with id: {request.Id}"
        );

        return _mapper.Map<ProductItemDetailDto>(productItem);
    }
}