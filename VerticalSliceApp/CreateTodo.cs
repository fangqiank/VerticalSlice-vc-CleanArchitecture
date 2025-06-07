using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using VerticalSliceApp.Data;
using VerticalSliceApp.Models;

namespace VerticalSliceApp
{
    public static class CreateTodo
    {
        public static RouteGroupBuilder MapCreateTodoEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (
                [FromBody] CreateTodoRequest request,
                [FromServices] AppDbContext db) =>
            {
                var todo = new Todo
                {
                    Title = request.Title,
                    IsCompleted = false
                };

                db.Todos.Add(todo);
                await db.SaveChangesAsync();

                return Results.Created($"/todos/{todo.Id}", todo);
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

        public sealed record CreateTodoRequest(string Title);
    }
}
