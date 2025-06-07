using CleanArchitectureApp.Dtos;
using MediatR;

namespace CleanArchitectureApp.Queries
{
    public sealed record SearchTodosQuery(string Term) : IRequest<IEnumerable<TodoDto>>;
}
