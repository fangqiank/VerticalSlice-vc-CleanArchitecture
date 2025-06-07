using FluentValidation;
using MediatR;
using VerticalSliceApp.Commands;
using VerticalSliceApp.Models;

namespace VerticalSliceApp
{
    public static class CreateTodo
    {
        public static RouteGroupBuilder MapCreateTodoEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (
                CreateTodoCommand command, 
                IMediator mediator) =>
            {
                var todoId = await mediator.Send(command);
                return Results.Created($"/todos/{todoId}", new { Id = todoId });
            })
            .WithName("CreateTodo")
            .Produces<Todo>(StatusCodes.Status201Created)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Create a new todo item",
                Description = "Creates a new todo item with the provided title"
            });

            return group;
        }
    }
}
