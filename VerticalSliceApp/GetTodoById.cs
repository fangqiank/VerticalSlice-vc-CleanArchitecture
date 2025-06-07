using MediatR;
using Microsoft.AspNetCore.Mvc;
using VerticalSliceApp.Data;
using VerticalSliceApp.Models;
using VerticalSliceApp.Queries;

namespace VerticalSliceApp
{
    public static class GetTodoById
    {
        public static RouteGroupBuilder MapGetTodoByIdEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{id}", async (
                int id, 
                IMediator mediator) =>
            {
                var query = new GetTodoByIdQuery(id);
                var todo = await mediator.Send(query);
                return todo != null ? Results.Ok(todo) : Results.NotFound();
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
