using AutoMapper;
using TodoApplication.Entities;
using TodoApplication.Models.DTOs;
using TodoApplication.Models.DTOs.Requests;
using TodoApplication.Models.DTOs.Responses;

namespace TodoApplication
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            this.CreateMap<UserEntity, UserAuthRequest>().ReverseMap();
            this.CreateMap<UserEntity, UserAuthResponse>().ReverseMap();
            this.CreateMap<TaskEntity, CreateTaskRequest>().ReverseMap();
            this.CreateMap<TaskEntity, CreateTaskResponse>().ReverseMap();
            this.CreateMap<TaskEntity, UpdateTaskRequest>().ReverseMap();
            this.CreateMap<TaskEntity, UpdateTaskResponse>().ReverseMap();
        }
    }
}
