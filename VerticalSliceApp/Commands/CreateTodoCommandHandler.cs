using MediatR;
using VerticalSliceApp.Data;
using VerticalSliceApp.Models;

namespace VerticalSliceApp.Commands
{
    public sealed class CreateTodoCommandHandler(AppDbContext dbContext) : IRequestHandler<CreateTodoCommand, int>
    {
        public async Task<int> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = new Todo
            {
                Title = request.Title,
                Description = request.Description,
                Priority = request.Priority,
                Tags = request.Tags ?? new List<string>(),
                CreatedAt = DateTime.UtcNow
            };

            dbContext.Todos.Add(todo);
            await dbContext.SaveChangesAsync(cancellationToken);

            return todo.Id;
        }
    }
}
