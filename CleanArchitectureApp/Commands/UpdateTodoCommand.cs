using CleanArchitectureApp.Dtos;
using CleanArchitectureApp.Models;
using MediatR;

namespace CleanArchitectureApp.Commands
{
    public sealed record UpdateTodoCommand(
        int Id,
        string? Title = null,
        string? Description = null,
        Priority? Priority = null,
        List<string>? Tags = null) : IRequest<TodoDto>;
}
