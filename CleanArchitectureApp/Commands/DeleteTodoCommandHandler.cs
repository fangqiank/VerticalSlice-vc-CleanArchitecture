using CleanArchitectureApp.Repositories;
using MediatR;

namespace CleanArchitectureApp.Commands
{
    public sealed class DeleteTodoCommandHandler(
     ITodoRepository repository)
     : IRequestHandler<DeleteTodoCommand, DeleteTodoResponse>
    {
        public async Task<DeleteTodoResponse> Handle(
            DeleteTodoCommand request,
            CancellationToken cancellationToken)
        {
            var todo = await repository.GetByIdAsync(request.Id, cancellationToken);
            if (todo is null)
            {
                return new DeleteTodoResponse(false, $"Todo with ID {request.Id} not found");
            }

            await repository.DeleteAsync(todo, cancellationToken);
            return new DeleteTodoResponse(true, $"Todo '{todo.Title}' deleted successfully");
        }
    }
}
