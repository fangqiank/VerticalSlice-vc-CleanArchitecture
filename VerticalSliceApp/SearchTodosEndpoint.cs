using MediatR;
using VerticalSliceApp.Models;
using VerticalSliceApp.Queries;

namespace VerticalSliceApp
{
    public static class SearchTodosEndpoint
    {
        public static RouteGroupBuilder MapSearchTodosEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/search", async (
               string term, 
               IMediator mediator) =>
            {
                if (string.IsNullOrWhiteSpace(term))
                {
                    return Results.BadRequest("Search term is required");
                }

                var query = new SearchTodosQuery(term);
                var todos = await mediator.Send(query);
                return Results.Ok(todos);
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
