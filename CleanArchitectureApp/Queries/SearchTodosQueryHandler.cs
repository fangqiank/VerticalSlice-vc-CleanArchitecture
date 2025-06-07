using AutoMapper;
using CleanArchitectureApp.Dtos;
using CleanArchitectureApp.Repositories;
using MediatR;

namespace CleanArchitectureApp.Queries
{
    public sealed class SearchTodosQueryHandler(
    ITodoRepository repository,
    IMapper mapper)
    : IRequestHandler<SearchTodosQuery, IEnumerable<TodoDto>>
    {
        public async Task<IEnumerable<TodoDto>> Handle(
            SearchTodosQuery request,
            CancellationToken cancellationToken)
        {
            var todos = await repository.SearchAsync(request.Term, cancellationToken);
            return mapper.Map<IEnumerable<TodoDto>>(todos);
        }
    }

}
