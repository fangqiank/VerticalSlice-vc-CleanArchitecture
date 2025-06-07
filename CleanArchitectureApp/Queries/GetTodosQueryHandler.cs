using AutoMapper;
using CleanArchitectureApp.Dtos;
using CleanArchitectureApp.Repositories;
using MediatR;

namespace CleanArchitectureApp.Queries
{
    public sealed class GetTodosQueryHandler(
    ITodoRepository todoRepository,
    IMapper mapper)
    : IRequestHandler<GetTodosQuery, IEnumerable<TodoDto>>
    {
        public async Task<IEnumerable<TodoDto>> Handle(
            GetTodosQuery request,
            CancellationToken cancellationToken)
        {
            var todos = await todoRepository.ListAsync(
                request.Completed,
                request.Priority,
                request.Tag,
                cancellationToken);

            return mapper.Map<IEnumerable<TodoDto>>(todos);
        }
    }
}
