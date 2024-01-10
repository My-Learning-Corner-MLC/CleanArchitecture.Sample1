using MediatR;
using Sample1.API.Infrastructure;
using Sample1.Application.Common.Models;
using Sample1.Application.TodoItems.Commands.CreateTodoItem;
using Sample1.Application.TodoItems.Commands.DeleteTodoItem;
using Sample1.Application.TodoItems.Commands.UpdateTodoItem;
using Sample1.Application.TodoItems.Commands.UpdateTodoItemDetail;
using Sample1.Application.TodoItems.Queries.GetTodoItemsWithPagination;

namespace Sample1.API.Endpoints;

public class TodoItems : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            // .RequireAuthorization()
            .MapGet(GetTodoItemsWithPagination)
            .MapPost(CreateTodoItem)
            .MapPut(UpdateTodoItem, "{id}")
            .MapPut(UpdateTodoItemDetail, "UpdateDetail/{id}")
            .MapDelete(DeleteTodoItem, "{id}");
    }

    public async Task<PaginatedList<TodoItemBriefDto>> GetTodoItemsWithPagination(ISender sender, [AsParameters] GetTodoItemsWithPaginationQuery query)
    {
        return await sender.Send(query);
    }

    public async Task<int> CreateTodoItem(ISender sender, CreateTodoItemCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<IResult> UpdateTodoItem(ISender sender, int id, UpdateTodoItemCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> UpdateTodoItemDetail(ISender sender, int id, UpdateTodoItemDetailCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteTodoItem(ISender sender, int id)
    {
        await sender.Send(new DeleteTodoItemCommand(id));
        return Results.NoContent();
    }
}
