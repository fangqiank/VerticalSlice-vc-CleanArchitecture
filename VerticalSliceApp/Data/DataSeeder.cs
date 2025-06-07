using Bogus;
using VerticalSliceApp.Models;

namespace VerticalSliceApp.Data
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(AppDbContext dbContext)
        {
            var todoFaker = new Faker<Todo>()
                .RuleFor(t => t.Title, f => f.Lorem.Sentence(3, 5))
                .RuleFor(t => t.IsCompleted, f => f.Random.Bool(0.3f)) // 30% 已完成
                .RuleFor(t => t.CreatedAt, f => f.Date.Past(2))
                .RuleFor(t => t.Description, f => f.Lorem.Paragraph().OrNull(f, 0.7f)) // 70% 有描述
                .RuleFor(t => t.Priority, f => f.PickRandom<Priority>().OrNull(f, 0.2f)) // 20% 无优先级
                .RuleFor(t => t.Tags, f => f.Lorem.Words(f.Random.Int(0, 3)).ToList());

            var todos = todoFaker.Generate(50);

            await dbContext.Todos.AddRangeAsync(todos); 
            await dbContext.SaveChangesAsync(); 
        }
    }

    public enum Priority
    {
        Low,
        Medium,
        High,
        Critical
    }
}
