using MediatR;

namespace VerticalSliceApp.Commands
{
    public sealed record DeleteTodoCommand(int Id) : IRequest<bool>;
}
