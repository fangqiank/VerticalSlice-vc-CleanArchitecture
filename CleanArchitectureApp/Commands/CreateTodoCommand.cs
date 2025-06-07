using CleanArchitectureApp.Models;
using MediatR;

namespace CleanArchitectureApp.Commands
{
    public sealed record CreateTodoCommand(
     string Title,
     string? Description = null,
     Priority? Priority = null,
     List<string>? Tags = null)
     : IRequest<CreateTodoResponse>;

    public sealed record CreateTodoResponse(int Id);
}
