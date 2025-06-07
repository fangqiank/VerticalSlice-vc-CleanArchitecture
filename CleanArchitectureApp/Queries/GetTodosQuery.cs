using CleanArchitectureApp.Dtos;
using CleanArchitectureApp.Models;
using MediatR;

namespace CleanArchitectureApp.Queries
{
    public sealed record GetTodosQuery(
    bool? Completed = null,
    Priority? Priority = null,
    string? Tag = null)
    : IRequest<IEnumerable<TodoDto>>;
}
