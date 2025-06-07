using MediatR;
using VerticalSliceApp.Dtos;

namespace VerticalSliceApp.Queries
{
    public sealed record SearchTodosQuery(string Term) : IRequest<List<TodoDto>>;
}
