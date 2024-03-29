using AutoMapper;
using MediatR;
using Sample1.Application.Common.Interfaces;
using Sample1.Application.Common.Models;

namespace Sample1.Application.ProductItems.Queries.GetProductItemsWithPagination;

public record GetProductItemsWithPaginationQuery : IRequest<PaginatedList<ProductItemBriefDto>>
{
    public bool IncludeTotalCount { get; set; } = false;

    public int PageNumber { get; init; } = 1;

    public int PageSize { get; init; } = 20;
}

public class GetProductItemsWithPaginationQueryHandler : IRequestHandler<GetProductItemsWithPaginationQuery, PaginatedList<ProductItemBriefDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductItemsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
         => (_unitOfWork, _mapper) = (unitOfWork, mapper);

    public async Task<PaginatedList<ProductItemBriefDto>> Handle(GetProductItemsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var products = await _unitOfWork.Products
            .GetAll(new()
            {
                Page = request.PageNumber,
                Size = request.PageSize,
                CancellationToken = cancellationToken  
            });

        int? count = null;
        if (request.IncludeTotalCount)
        {
            count = await _unitOfWork.Products.CountAll(cancellationToken: cancellationToken);
        }

        var productItemBriefs = _mapper.Map<List<ProductItemBriefDto>>(products);
        return new PaginatedList<ProductItemBriefDto>(productItemBriefs.ToList().AsReadOnly(), count, request.PageNumber, request.PageSize);
    }
}