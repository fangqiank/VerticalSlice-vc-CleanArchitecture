using CleanArchitectureApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureApp.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options)
    {
        public DbSet<Todo> Todos => Set<Todo>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>(entity =>
            {
                entity.Property(t => t.Title).HasMaxLength(100);
                entity.Property(t => t.Description).HasMaxLength(500);

                entity.Property(e => e.Tags)
                    .HasConversion(
                        v => string.Join(',', v),
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
            });
        }
    }
}
