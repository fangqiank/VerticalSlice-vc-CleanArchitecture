using CleanArchitectureApp.Data;
using CleanArchitectureApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureApp.Repositories
{
    public class TodoRepository(AppDbContext context) : ITodoRepository
    {
        public async Task AddAsync(Todo todo, CancellationToken cancellationToken = default)
        {
            await context.Todos.AddAsync(todo, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Todo todo, CancellationToken cancellationToken = default)
        {
            context.Todos.Remove(todo);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Todo?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await context.Todos
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Todo>> ListAsync(
            bool? completed,
            Priority? priority,
            string? tag,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Todo> query = context.Todos;

            if (completed.HasValue)
                query = query.Where(t => t.IsCompleted == completed.Value);

            if (priority.HasValue)
                query = query.Where(t => t.Priority == priority.Value);

            if (!string.IsNullOrEmpty(tag))
                query = query.Where(t => t.Tags.Contains(tag));

            return await query
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync(cancellationToken);
        }

        public async Task ToggleCompletedAsync(int id, CancellationToken cancellationToken = default)
        {
            var todo = await context.Todos.FindAsync([id], cancellationToken);
            if (todo != null)
            {
                todo.IsCompleted = !todo.IsCompleted;
                await context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<IEnumerable<Todo>> SearchAsync(string term, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(term))
                return Enumerable.Empty<Todo>();

            var normalizedTerm = term.ToLower();

            return await context.Todos
                .Where(t =>
                    t.Title.ToLower().Contains(normalizedTerm) ||
                    (t.Description != null && t.Description.ToLower().Contains(normalizedTerm)) ||
                    t.Tags.Any(tag => tag.ToLower().Contains(normalizedTerm)))
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task UpdateAsync(Todo todo, CancellationToken cancellationToken = default)
        {
            var existingTodo = await context.Todos.FindAsync([todo.Id], cancellationToken);
            if (existingTodo != null)
            {
                context.Entry(existingTodo).CurrentValues.SetValues(todo);
                await context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
