using MediatR;
using Microsoft.EntityFrameworkCore;
using VerticalSliceApp.Data;

namespace VerticalSliceApp.Commands
{
    public class DeleteTodoCommandHandler(AppDbContext dbContext) : IRequestHandler<DeleteTodoCommand, bool>
    {
        public async Task<bool> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = await dbContext.Todos
                .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            if (todo == null)
                return false;

            dbContext.Todos.Remove(todo);
            await dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
