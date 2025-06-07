using MediatR;
using Microsoft.EntityFrameworkCore;
using VerticalSliceApp.Data;
using VerticalSliceApp.Dtos;

namespace VerticalSliceApp.Queries
{
    public class GetTodoByIdQueryHandler(AppDbContext dbContext) : IRequestHandler<GetTodoByIdQuery, TodoDto?>
    {
        public async Task<TodoDto?> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
        {
            var todo = await dbContext.Todos
                 .AsNoTracking()
                 .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            if (todo == null)
                return null;

            return new TodoDto(
                todo.Id,
                todo.Title,
                todo.IsCompleted,
                todo.CreatedAt,
                todo.Description,
                todo.Priority,
                todo.Tags);
        }
    }
}
