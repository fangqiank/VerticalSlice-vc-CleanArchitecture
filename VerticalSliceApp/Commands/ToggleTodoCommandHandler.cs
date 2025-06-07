using MediatR;
using Microsoft.EntityFrameworkCore;
using VerticalSliceApp.Data;
using VerticalSliceApp.Dtos;

namespace VerticalSliceApp.Commands
{
    public class ToggleTodoCommandHandler(AppDbContext dbContext) : IRequestHandler<ToggleTodoCommand, TodoDto?>
    {
        public async Task<TodoDto?> Handle(ToggleTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = await dbContext.Todos
                .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            if (todo == null)
                return null;

            todo.IsCompleted = !todo.IsCompleted;

            await dbContext.SaveChangesAsync(cancellationToken);

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
