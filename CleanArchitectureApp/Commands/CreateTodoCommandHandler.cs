using CleanArchitectureApp.Models;
using CleanArchitectureApp.Repositories;
using MediatR;

namespace CleanArchitectureApp.Commands
{
    public sealed class CreateTodoCommandHandler(
    ITodoRepository todoRepository) 
    : IRequestHandler<CreateTodoCommand, CreateTodoResponse>
{
    public async Task<CreateTodoResponse> Handle(
        CreateTodoCommand request, 
        CancellationToken cancellationToken)
    {
        var todo = new Todo
        {
            Title = request.Title,
            Description = request.Description,
            Priority = request.Priority,
            Tags = request.Tags ?? new List<string>()
        };

        await todoRepository.AddAsync(todo, cancellationToken);

        return new CreateTodoResponse(todo.Id);
    }
}
}
