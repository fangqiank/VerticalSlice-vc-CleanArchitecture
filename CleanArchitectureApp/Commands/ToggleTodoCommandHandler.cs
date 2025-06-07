using AutoMapper;
using CleanArchitectureApp.Dtos;
using CleanArchitectureApp.Repositories;
using MediatR;

namespace CleanArchitectureApp.Commands
{
    public sealed class ToggleTodoCommandHandler(
    ITodoRepository repository,
    IMapper mapper)
    : IRequestHandler<ToggleTodoCommand, TodoDto>
    {
        public async Task<TodoDto> Handle(
            ToggleTodoCommand request,
            CancellationToken cancellationToken)
        {
            var todo = await repository.GetByIdAsync(request.Id, cancellationToken);
            if (todo is null)
            {
                throw new Exception($"Todo with ID {request.Id} not found");
            }

            todo.IsCompleted = !todo.IsCompleted;
            await repository.UpdateAsync(todo, cancellationToken);

            return mapper.Map<TodoDto>(todo);
        }
    }
}
