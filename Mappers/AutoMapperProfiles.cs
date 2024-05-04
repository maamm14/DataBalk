using AutoMapper;
using UserTaskApi.Models.Domain;
using UserTaskApi.Models.DTO.Tasks;

namespace UserTaskApi.Mappers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<TaskDomain, TasksDto>().ReverseMap();
            CreateMap<CreateTaskRequestDto, TaskDomain>().ReverseMap();
            CreateMap<UpdateTaskRequestDto, TaskDomain>().ReverseMap();
            CreateMap<TaskDomain, TasksDto>()
            .ForMember(dest => dest.AssigneeId, opt => opt.MapFrom(src => src.AssignedUser));
        }
    }
}