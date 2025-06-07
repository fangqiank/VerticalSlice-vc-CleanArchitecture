using Microsoft.AspNetCore.Mvc;
using VerticalSliceApp.Data;
using VerticalSliceApp.Models;

namespace VerticalSliceApp
{
    public static class ToggleTodoEndpoint
    {
        public static RouteGroupBuilder MapToggleTodoEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("/{id}/toggle", async (
            [FromRoute] int id,
            [FromServices] AppDbContext db) =>
            {
                var todo = await db.Todos.FindAsync(id);
                if (todo is null)
                {
                    return Results.NotFound();
                }

                todo.IsCompleted = !todo.IsCompleted;
                await db.SaveChangesAsync();

                return Results.Ok(todo);
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
