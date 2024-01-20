using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Sample1.API.Infrastructure;
using Sample1.Application.Common.Exceptions;
using Sample1.Application.Common.Models;
using Sample1.Application.ProductItems.Commands.CreateProductItem;
using Sample1.Application.ProductItems.Commands.UpdateProductItem;
using Sample1.Application.ProductItems.Commands.DeleteProductItem;
using Sample1.Application.ProductItems.Queries.GetProductItemDetail;
using Sample1.Application.ProductItems.Queries.GetProductItemsWithPagination;
using Sample1.Application.Common.Constants;

namespace Sample1.API.Endpoints;

public class ProductItems : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this, "products")
            .MapGet(GetProductItemDetail, "/{id}")
            .MapPost(GetProductItemsWithPagination, "/query")     
            .MapPut(UpdateProductItem, "/{id}")
            .MapDelete(DeleteProductItem, "/{id}")
            .MapPost(CreateProductItem);
    }

    public async Task<Results<Ok<ProductItemDetailDto>, NotFound, BadRequest>> GetProductItemDetail(ISender sender, int id)
    {
        if (id < 0) throw new ValidationException("Id at least greater than or equal to 0.");

        var item = await sender.Send(new GetProductItemDetailQuery { Id = id });

        return TypedResults.Ok(item);
    }

    public async Task<Results<Ok<PaginatedList<ProductItemBriefDto>>, BadRequest>> GetProductItemsWithPagination(ISender sender, GetProductItemsWithPaginationQuery query)
    {
        return TypedResults.Ok(await sender.Send(query));
    }
    
    public async Task<IResult> CreateProductItem(ISender sender, CreateProductItemCommand command)
    {
        return Results.Ok(await sender.Send(command));
    }

    public async Task<IResult> UpdateProductItem(ISender sender, int id, UpdateProductItemCommand command)
    {
        if (id != command.Id) 
            throw new ConflictException(
                errorMessage: ExceptionConst.ErrorMessage.RESOURCE_CONFLICT,
                errorDescription: ExceptionConst.ErrorDescription.ID_CONFLICT);
        
        await sender.Send(command);

        return Results.NoContent();
    }

    public async Task<IResult> DeleteProductItem(ISender sender, int id)
    {
        await sender.Send(new DeleteProductItemCommand() { Id = id });
        return Results.NoContent();
    }
}