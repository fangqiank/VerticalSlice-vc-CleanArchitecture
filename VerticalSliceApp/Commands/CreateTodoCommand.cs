using MediatR;
using VerticalSliceApp.Models;

namespace VerticalSliceApp.Commands
{
    public sealed record CreateTodoCommand(
        string Title,
        string? Description = null,
        Priority? Priority = null,
        List<string>? Tags = null) : IRequest<int>;

}
