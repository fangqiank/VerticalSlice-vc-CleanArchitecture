using VerticalSliceApp.Models;

namespace VerticalSliceApp.Dtos
{
    public sealed record TodoDto(
        int Id,
        string Title,
        bool IsCompleted,
        DateTime CreatedAt,
        string? Description,
        Priority? Priority,
        List<string> Tags);
}
