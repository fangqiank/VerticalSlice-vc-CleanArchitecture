using CleanArchitectureApp.Dtos;
using MediatR;

namespace CleanArchitectureApp.Queries
{
    public sealed record GetTodoByIdQuery(int Id) : IRequest<TodoDto>;

}
