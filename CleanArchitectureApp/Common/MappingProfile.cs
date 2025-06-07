using AutoMapper;
using CleanArchitectureApp.Commands;
using CleanArchitectureApp.Dtos;
using CleanArchitectureApp.Models;

namespace CleanArchitectureApp.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Todo, TodoDto>();
            CreateMap<CreateTodoCommand, Todo>()
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags ?? new List<string>()));
        }
    }
}
