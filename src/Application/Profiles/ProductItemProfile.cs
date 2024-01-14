using AutoMapper;
using Sample1.Application.ProductItems.Queries.GetProductItemDetail;
using Sample1.Application.ProductItems.Queries.GetProductItemsWithPagination;
using Sample1.Domain.Entities;

namespace Sample1.Application.Profiles;

public class ProductItemProfile : Profile
{
    public ProductItemProfile()
    {
        CreateMap<ProductItem, ProductItemDetailDto>();
        CreateMap<ProductItem, ProductItemBriefDto>();
    }
}