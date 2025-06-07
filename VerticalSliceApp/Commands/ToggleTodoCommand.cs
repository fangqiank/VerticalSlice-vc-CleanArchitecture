using MediatR;
using VerticalSliceApp.Dtos;

namespace VerticalSliceApp.Commands
{
    public sealed record ToggleTodoCommand(int Id) : IRequest<TodoDto?>;
}
