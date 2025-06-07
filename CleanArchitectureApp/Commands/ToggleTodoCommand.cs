using CleanArchitectureApp.Dtos;
using MediatR;

namespace CleanArchitectureApp.Commands
{
    public sealed record ToggleTodoCommand(int Id) : IRequest<TodoDto>;
}
