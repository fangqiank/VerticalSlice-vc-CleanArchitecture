namespace CleanArchitectureApp.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? Description { get; set; }
        public Priority? Priority { get; set; }
        public List<string> Tags { get; set; } = new();
    }

    public enum Priority
    {
        Low,
        Medium,
        High,
        Critical
    }
}
