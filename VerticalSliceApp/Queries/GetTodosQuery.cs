using MediatR;
using VerticalSliceApp.Dtos;
using VerticalSliceApp.Models;

namespace VerticalSliceApp.Queries
{
    public sealed record GetTodosQuery(
        bool? Completed = null,
        Priority? Priority = null,
        string? Tag = null) : IRequest<List<TodoDto>>;

}
