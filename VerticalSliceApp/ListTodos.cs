using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VerticalSliceApp.Data;
using VerticalSliceApp.Models;

namespace VerticalSliceApp
{
    public static class ListTodos
    {
        public static RouteGroupBuilder MapListTodosEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (
                [FromQuery] bool? completed,
                [FromQuery] Priority? priority,
                [FromQuery] string? tag,
                [FromServices] AppDbContext db) =>
            {
                IQueryable<Todo> query = db.Todos;

                if (completed.HasValue)
                {
                    query = query.Where(t => t.IsCompleted == completed.Value);
                }

                if (priority.HasValue)
                {
                    query = query.Where(t => t.Priority == priority.Value);
                }

                if (!string.IsNullOrEmpty(tag))
                {
                    query = query.Where(t => t.Tags.Contains(tag));
                }

                return await query
                    .OrderByDescending(t => t.CreatedAt)
                    .ToListAsync();
            })
            .WithName("GetAllTodos")
            .Produces<List<Todo>>(StatusCodes.Status200OK)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Get all todo items",
                Description = "Returns a list of all todo items with optional filtering"
            });

            return group;
        }
    }
}
