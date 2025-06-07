using MediatR;
using VerticalSliceApp.Dtos;

namespace VerticalSliceApp.Queries
{
    public sealed record GetTodoByIdQuery(int Id) : IRequest<TodoDto?>;
}
