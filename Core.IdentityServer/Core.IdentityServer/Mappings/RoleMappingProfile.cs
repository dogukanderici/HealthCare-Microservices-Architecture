using AutoMapper;
using Core.IdentityServer.Dtos.RoleDtos;
using Core.IdentityServer.Models;
using Core.IdentityServer.Services.RabbitMQ.Events.Roles;

namespace Core.IdentityServer.Mappings
{
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<ApplicationRole, CreateRoleDto>().ReverseMap();
            CreateMap<ApplicationRole, RoleCreatedEvent>().ReverseMap();
            CreateMap<ApplicationRole, RoleUpdatedEvent>().ReverseMap();
        }
    }
}