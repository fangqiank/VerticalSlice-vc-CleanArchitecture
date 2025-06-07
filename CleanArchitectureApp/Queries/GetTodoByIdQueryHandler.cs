using AutoMapper;
using CleanArchitectureApp.Dtos;
using CleanArchitectureApp.Repositories;
using MediatR;

namespace CleanArchitectureApp.Queries
{
    public sealed class GetTodoByIdQueryHandler(
        ITodoRepository repository,
        IMapper mapper)
    : IRequestHandler<GetTodoByIdQuery, TodoDto>
    {
        public async Task<TodoDto> Handle(
            GetTodoByIdQuery request,
            CancellationToken cancellationToken)
        {
            var todo = await repository.GetByIdAsync(request.Id, cancellationToken);
            return mapper.Map<TodoDto>(todo);
        }
    }
}
