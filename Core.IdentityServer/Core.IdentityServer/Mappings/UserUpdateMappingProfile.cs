using AutoMapper;
using Core.IdentityServer.Dtos.UserDtos;
using Core.IdentityServer.Models;

namespace Core.IdentityServer.Mappings
{
    public class UserUpdateMappingProfile : Profile
    {
        public UserUpdateMappingProfile()
        {
            CreateMap<ApplicationUser, UserUpdateDto>().ReverseMap();
        }
    }
}