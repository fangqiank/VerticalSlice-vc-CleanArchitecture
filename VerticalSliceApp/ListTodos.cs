using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VerticalSliceApp.Data;
using VerticalSliceApp.Models;
using VerticalSliceApp.Queries;

namespace VerticalSliceApp
{
    public static class ListTodos
    {
        public static RouteGroupBuilder MapListTodosEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (
                bool? completed,
                Priority? priority,
                string? tag,
                IMediator mediator) =>
            {
                var query = new GetTodosQuery(completed, priority, tag);
                var todos = await mediator.Send(query);
                return Results.Ok(todos);
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
