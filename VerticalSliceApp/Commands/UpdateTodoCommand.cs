using MediatR;
using VerticalSliceApp.Dtos;
using VerticalSliceApp.Models;

namespace VerticalSliceApp.Commands
{
    public sealed record UpdateTodoCommand(
         int Id,
         string? Title = null,
         string? Description = null,
         Priority? Priority = null,
         List<string>? Tags = null) : IRequest<TodoDto?>;
}
