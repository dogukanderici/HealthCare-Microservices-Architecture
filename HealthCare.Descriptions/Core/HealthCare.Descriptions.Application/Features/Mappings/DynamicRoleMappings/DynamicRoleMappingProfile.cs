using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediators.DynamicRoles.Commands;
using HealthCare.Descriptions.Application.Features.Mediators.DynamicRoles.Results;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mappings.DynamicRoleMappings
{
    public class DynamicRoleMappingProfile : Profile
    {
        public DynamicRoleMappingProfile()
        {
            CreateMap<DynamicRole, GetDynamicRolesQueryResult>().ReverseMap();
            CreateMap<DynamicRole, GetDynamicRoleByIdQueryResult>().ReverseMap();
            CreateMap<DynamicRole, GetDynamicRolesByFilterQueryResult>().ReverseMap();
            CreateMap<DynamicRole, CreateDynamicRoleCommand>().ReverseMap();
            CreateMap<DynamicRole, UpdateDynamicRoleCommand>().ReverseMap();
            CreateMap<DynamicRole, DeleteDynamicRoleCommand>().ReverseMap();
        }
    }
}