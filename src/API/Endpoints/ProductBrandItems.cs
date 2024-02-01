using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Sample1.API.Infrastructure;
using Sample1.Application.Common.Exceptions;
using Sample1.Application.Common.Constants;
using Sample1.Application.ProductBrandItems.Commands.CreateProductBrandItem;
using Sample1.Application.ProductBrandItems.Commands.UpdateProductBrandItem;
using Sample1.Application.ProductBrandItems.Commands.DeleteProductBrandItem;

namespace Sample1.API.Endpoints;

public class ProductBrandItems : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this, groupName: "brands", tagName: "BRAND APIs")
            .MapPost(CreateProductBrandItem)
            .MapPut(UpdateProductBrandItem, "/{id}")
            .MapDelete(DeleteProductBrandItem, "/{id}");
    }
    
    public async Task<Results<Ok<int>, BadRequest>> CreateProductBrandItem(ISender sender, CreateProductBrandItemCommand command)
    {
        return TypedResults.Ok(await sender.Send(command));
    }

    public async Task<Results<NoContent, BadRequest, NotFound>> UpdateProductBrandItem(ISender sender, int id, UpdateProductBrandItemCommand command)
    {
        if (id != command.Id) 
            throw new ValidationException(
                errorDescription: $"The product brand id - {id} in url and id - {command.Id} in command do not match");
        
        await sender.Send(command);

        return TypedResults.NoContent();
    }

    public async Task<Results<NoContent, NotFound, BadRequest>> DeleteProductBrandItem(ISender sender, int id)
    {
        if (id < 0) throw new ValidationException(BrandConst.ErrorMessages.BRAND_ID_AT_LEAST_GREATER_THAN_0);

        await sender.Send(new DeleteProductIBrandItemCommand{ Id = id });
        
        return TypedResults.NoContent();
    }
}