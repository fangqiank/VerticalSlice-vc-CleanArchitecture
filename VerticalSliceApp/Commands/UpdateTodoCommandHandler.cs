using MediatR;
using Microsoft.EntityFrameworkCore;
using VerticalSliceApp.Data;
using VerticalSliceApp.Dtos;

namespace VerticalSliceApp.Commands
{
    public class UpdateTodoCommandHandler(AppDbContext dbContext) : IRequestHandler<UpdateTodoCommand, TodoDto?>
    {
        public async Task<TodoDto?> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = await dbContext.Todos
                 .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            if (todo == null)
                return null;

            // 部分更新
            if (request.Title != null) todo.Title = request.Title;
            if (request.Description != null) todo.Description = request.Description;
            if (request.Priority != null) todo.Priority = request.Priority;
            if (request.Tags != null) todo.Tags = request.Tags;

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
