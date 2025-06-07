using MediatR;
using VerticalSliceApp.Commands;

namespace VerticalSliceApp
{
    public static class DeleteTodo
    {
        public static RouteGroupBuilder MapDeleteTodoEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/{id}", async (
                int id, 
                IMediator mediator) =>
            {
                var command = new DeleteTodoCommand(id);
                var result = await mediator.Send(command);
                return result ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteTodo")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Delete a todo item",
                Description = "Delete a  todo item with the provided Id"
            });

            return group;
        }
    }
}
