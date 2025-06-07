using CleanArchitectureApp.Commands;
using CleanArchitectureApp.Dtos;
using CleanArchitectureApp.Models;
using CleanArchitectureApp.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureApp
{
    public static class TodoEndpoints
    {
        public static void MapTodoEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/todos")
               .WithTags("Todo Management");

            group.MapPost("/", async (
                [FromBody] CreateTodoCommand command,
                [FromServices] ISender sender) =>
            {
                var result = await sender.Send(command);
                return Results.Created($"/todos/{result.Id}", result);
            })
            .WithName("CreateTodo")
            .Produces<CreateTodoResponse>(StatusCodes.Status201Created)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Create a new todo",
                Description = "Creates a new todo item"
            });

            group.MapGet("/", async (
                [FromQuery] bool? completed,
                [FromQuery] Priority? priority,
                [FromQuery] string? tag,
                [FromServices] ISender sender) =>
            {
                var query = new GetTodosQuery(completed, priority, tag);
                var todos = await sender.Send(query);
                return Results.Ok(todos);
            })
            .WithName("GetTodos")
            .Produces<IEnumerable<TodoDto>>(StatusCodes.Status200OK)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Get todos",
                Description = "Returns a list of todos with optional filtering"
            });

            group.MapGet("/{id}", async (
            [FromRoute] int id,
            [FromServices] ISender sender) =>
            {
                var query = new GetTodoByIdQuery(id);
                var todo = await sender.Send(query);
                return todo != null ? Results.Ok(todo) : Results.NotFound();
            })
            .WithName("GetTodoById")
            .Produces<TodoDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Get todo by ID",
                Description = "Returns a single todo item by its ID"
            });

            group.MapPut("/{id}/toggle", async (
            [FromRoute] int id,
            [FromServices] ISender sender) =>
            {
                var command = new ToggleTodoCommand(id);
                var result = await sender.Send(command);
                return Results.Ok(result);
            })
            .WithName("ToggleTodo")
            .Produces<TodoDto>(StatusCodes.Status200OK)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Toggle todo status",
                Description = "Toggles the completion status of a todo item"
            });

            group.MapGet("/search", async (
            [FromQuery] string term,
            [FromServices] ISender sender) =>
            {
                if (string.IsNullOrWhiteSpace(term))
                {
                    return Results.BadRequest("Search term is required");
                }

                var query = new SearchTodosQuery(term);
                var results = await sender.Send(query);
                return Results.Ok(results);
            })
            .WithName("SearchTodos")
            .Produces<IEnumerable<TodoDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Search todos",
                Description = "Searches todos by title, description or tags"
            });

            group.MapPut("/{id}", async (
            [FromRoute] int id,
            [FromBody] UpdateTodoCommand command,
            [FromServices] ISender sender) =>
            {
                // 确保路径ID与命令ID一致
                if (id != command.Id)
                {
                    return Results.BadRequest("ID in route does not match ID in body");
                }

                var result = await sender.Send(command);
                return Results.Ok(result);
            })
            .WithName("UpdateTodo")
            .Produces<TodoDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Update todo",
                Description = "Updates an existing todo item"
            });

            group.MapDelete("/{id}", async (
            [FromRoute] int id,
            [FromServices] ISender sender) =>
            {
                var command = new DeleteTodoCommand(id);
                var result = await sender.Send(command);

                return result.Success
                    ? Results.Ok(result)
                    : Results.NotFound(result);
            })
            .WithName("DeleteTodo")
            .Produces<DeleteTodoResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Delete todo",
                Description = "Deletes a todo item by its ID"
            });
        }
    }
}
