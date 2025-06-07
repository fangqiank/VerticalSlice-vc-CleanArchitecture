using CleanArchitectureApp.Models;

namespace CleanArchitectureApp.Repositories
{
    public interface ITodoRepository
    {
        Task AddAsync(Todo todo, CancellationToken cancellationToken = default);
        Task DeleteAsync(Todo todo, CancellationToken cancellationToken = default);
        Task<Todo?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Todo>> ListAsync(bool? completed, Priority? priority, string? tag, CancellationToken cancellationToken = default);
        Task<IEnumerable<Todo>> SearchAsync(string term, CancellationToken cancellationToken = default);
        Task ToggleCompletedAsync(int id, CancellationToken cancellationToken = default);
        Task UpdateAsync(Todo todo, CancellationToken cancellationToken = default);
    }
}