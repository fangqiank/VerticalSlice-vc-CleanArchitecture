using MediatR;

namespace CleanArchitectureApp.Commands
{
    public sealed record DeleteTodoCommand(int Id) : IRequest<DeleteTodoResponse>;
    public sealed record DeleteTodoResponse(bool Success, string Message);
}
