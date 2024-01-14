using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Sample1.Application.Common.Interfaces;
using Sample1.Application.Common.Mappings;
using Sample1.Application.Common.Models;

namespace Sample1.Application.ProductItems.Queries.GetProductItemsWithPagination;

public record GetProductItemsWithPaginationQuery : IRequest<PaginatedList<ProductItemBriefDto>>
{
    public int PageNumber { get; init; } = 1;

    public int PageSize { get; init; } = 20;
}

public class GetProductItemsWithPaginationQueryHandler : IRequestHandler<GetProductItemsWithPaginationQuery, PaginatedList<ProductItemBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProductItemsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
         => (_context, _mapper) = (context, mapper);

    public async Task<PaginatedList<ProductItemBriefDto>> Handle(GetProductItemsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.ProductItems
            .ProjectTo<ProductItemBriefDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}