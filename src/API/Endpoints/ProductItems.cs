using MediatR;
using Sample1.API.Infrastructure;
using Sample1.Application.Common.Exceptions;
using Sample1.Application.ProductItems.Queries.GetProductItemDetail;
using Sample1.Application.ProductItems.Queries.GetProductItemsWithPagination;

namespace Sample1.API.Endpoints;

public class ProductItems : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this, "products")
            .MapGet(GetProductItemDetail, "/{id}")
            .MapPost(GetProductItemsWithPagination);
    }

    public async Task<IResult> GetProductItemDetail(ISender sender, int id)
    {
        if (id < 0) throw new ValidationException("Id at least greater than or equal to 0.");

        var item = await sender.Send(new GetProductItemDetailQuery { Id = id });

        return Results.Ok(item);
    }

    public async Task<IResult> GetProductItemsWithPagination(ISender sender, GetProductItemsWithPaginationQuery query)
    {
        return Results.Ok(await sender.Send(query));
    }
}