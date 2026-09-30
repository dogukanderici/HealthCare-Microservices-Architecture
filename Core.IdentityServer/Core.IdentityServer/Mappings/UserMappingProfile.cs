using AutoMapper;
using Core.IdentityServer.Dtos.UserDtos;
using Core.IdentityServer.Models;
using Core.IdentityServer.Services.RabbitMQ.Events;

namespace Core.IdentityServer.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<ApplicationUser, RegisterDto>().ReverseMap();
            CreateMap<ApplicationUser, UserCreatedEvent>().ReverseMap();

            CreateMap<ApplicationUser, UserUpdateDto>().ReverseMap();
            CreateMap<ApplicationUser, UserUpdatedEvent>().ReverseMap();
        }
    }
}