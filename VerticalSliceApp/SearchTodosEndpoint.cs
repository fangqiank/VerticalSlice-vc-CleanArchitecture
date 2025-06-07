using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VerticalSliceApp.Data;
using VerticalSliceApp.Models;

namespace VerticalSliceApp
{
    public static class SearchTodosEndpoint
    {
        public static RouteGroupBuilder MapSearchTodosEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/search", async (
                [FromQuery] string term,
                [FromServices] AppDbContext db) =>
            {
                if (string.IsNullOrWhiteSpace(term))
                {
                    return Results.BadRequest("Search term is required");
                }

                var normalizedTerm = term.ToLower();

                var results = await db.Todos
                   .Where(t =>
                       t.Title.ToLower().Contains(normalizedTerm) ||
                       (t.Description != null && t.Description.ToLower().Contains(normalizedTerm)) ||
                       t.Tags.Any(tag => tag.ToLower().Contains(normalizedTerm)))
                   .OrderByDescending(t => t.CreatedAt)
                   .ToListAsync();

                return Results.Ok(results);
            })
            .WithName("SearchTodos")
            .Produces<List<Todo>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Search for todo items",
                Description = "Searches todos by title, description or tags"
            });
            return group;
        }
    }
}
