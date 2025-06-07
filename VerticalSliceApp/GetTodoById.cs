using Microsoft.AspNetCore.Mvc;
using VerticalSliceApp.Data;
using VerticalSliceApp.Models;

namespace VerticalSliceApp
{
    public static class GetTodoById
    {
        public static RouteGroupBuilder MapGetTodoByIdEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{id}", async (
            [FromRoute] int id,
            [FromServices] AppDbContext db) =>
            {
                return await db.Todos.FindAsync(id)
                    is Todo todo
                        ? Results.Ok(todo)
                        : Results.NotFound();
            })
            .WithName("GetTodoById")
            .Produces<Todo>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Get a todo item by ID",
                Description = "Returns a specific todo item with all details"
            });

            return group;
        }
    }
}
