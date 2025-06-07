using MediatR;
using VerticalSliceApp.Commands;
using VerticalSliceApp.Dtos;

namespace VerticalSliceApp
{
    public static class UpdateTodo
    {
        public static RouteGroupBuilder MapUpdateTodoEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("/{id}", async (
                int id, 
                UpdateTodoCommand command, 
                IMediator mediator) =>
            {
                if (id != command.Id)
                    return Results.BadRequest("ID in route does not match ID in body");

                var todo = await mediator.Send(command);
                return todo != null ? Results.Ok(todo) : Results.NotFound();
            })
            .WithName("UpdateTodo")
            .Produces<TodoDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Update an existed todo item",
                Description = "Updates an existed todo item with the provided title"
            });

            return group;
        }
    }
}
