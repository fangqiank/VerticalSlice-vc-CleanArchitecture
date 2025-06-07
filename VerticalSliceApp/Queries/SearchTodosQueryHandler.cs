using MediatR;
using Microsoft.EntityFrameworkCore;
using VerticalSliceApp.Data;
using VerticalSliceApp.Dtos;

namespace VerticalSliceApp.Queries
{
    public class SearchTodosQueryHandler(AppDbContext dbContext) : IRequestHandler<SearchTodosQuery, List<TodoDto>>
    {
        public async Task<List<TodoDto>> Handle(SearchTodosQuery request, CancellationToken cancellationToken)
        {
            var normalizedTerm = request.Term.ToLower();

            return await dbContext.Todos
                .Where(t =>
                    t.Title.ToLower().Contains(normalizedTerm) ||
                    (t.Description != null && t.Description.ToLower().Contains(normalizedTerm)) ||
                    t.Tags.Any(tag => tag.ToLower().Contains(normalizedTerm)))
                .OrderByDescending(t => t.CreatedAt)
                .Select(t => new TodoDto(
                    t.Id,
                    t.Title,
                    t.IsCompleted,
                    t.CreatedAt,
                    t.Description,
                    t.Priority,
                    t.Tags))
                .ToListAsync(cancellationToken);
        }
    }
}
