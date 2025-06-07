using MediatR;
using VerticalSliceApp.Commands;
using VerticalSliceApp.Models;

namespace VerticalSliceApp
{
    public static class ToggleTodoEndpoint
    {
        public static RouteGroupBuilder MapToggleTodoEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("/{id}/toggle", async (
            int id, 
            IMediator mediator) =>
            {
                var command = new ToggleTodoCommand(id);
                var todo = await mediator.Send(command);
                return todo != null ? Results.Ok(todo) : Results.NotFound();
            })
            .WithName("ToggleTodo")
            .Produces<Todo>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Toggle todo completion status",
                Description = "Toggles the completion status of a todo item"
            });

            return group;
        }
    }
}
