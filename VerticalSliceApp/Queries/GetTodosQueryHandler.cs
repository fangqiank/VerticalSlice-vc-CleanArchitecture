using MediatR;
using Microsoft.EntityFrameworkCore;
using VerticalSliceApp.Data;
using VerticalSliceApp.Dtos;
using VerticalSliceApp.Models;

namespace VerticalSliceApp.Queries
{
    public class GetTodosQueryHandler(AppDbContext dbContext) : IRequestHandler<GetTodosQuery, List<TodoDto>>
    {
        public async Task<List<TodoDto>> Handle(GetTodosQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Todo> query = dbContext.Todos;

            if (request.Completed.HasValue)
                query = query.Where(t => t.IsCompleted == request.Completed.Value);

            if (request.Priority.HasValue)
                query = query.Where(t => t.Priority == request.Priority.Value);

            if (!string.IsNullOrEmpty(request.Tag))
                query = query.Where(t => t.Tags.Contains(request.Tag));

            return await query
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
