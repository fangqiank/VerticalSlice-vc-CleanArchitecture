using AutoMapper;
using CleanArchitectureApp.Dtos;
using CleanArchitectureApp.Repositories;
using MediatR;

namespace CleanArchitectureApp.Commands
{
    public sealed class UpdateTodoCommandHandler(
    ITodoRepository repository,
    IMapper mapper)
    : IRequestHandler<UpdateTodoCommand, TodoDto>
    {
        public async Task<TodoDto> Handle(
            UpdateTodoCommand request,
            CancellationToken cancellationToken)
        {
            var todo = await repository.GetByIdAsync(request.Id, cancellationToken);
            if (todo is null)
            {
                throw new Exception($"Todo with ID {request.Id} not found");
            }

            // 部分更新
            if (request.Title != null) todo.Title = request.Title;
            if (request.Description != null) todo.Description = request.Description;
            if (request.Priority != null) todo.Priority = request.Priority;
            if (request.Tags != null) todo.Tags = request.Tags;

            await repository.UpdateAsync(todo, cancellationToken);
            return mapper.Map<TodoDto>(todo);
        }
    }
}
