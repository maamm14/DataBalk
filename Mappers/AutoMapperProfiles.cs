using AutoMapper;
using UserTaskApi.Models.Domain;
using UserTaskApi.Models.DTO.Tasks;
using UserTaskApi.Models.DTO.User;

namespace UserTaskApi.Mappers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<TaskDomain, TasksDto>().ReverseMap();
            CreateMap<CreateTaskRequestDto, TaskDomain>().ReverseMap();
            CreateMap<UpdateTaskRequestDto, TaskDomain>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserRegisterDto, User>().ReverseMap();
        }
    }
}