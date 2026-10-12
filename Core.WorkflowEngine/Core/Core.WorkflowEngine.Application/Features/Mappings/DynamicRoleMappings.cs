using AutoMapper;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.DynamicRoleCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Results.DynamicRoleResults;
using Core.WorkflowEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mappings
{
    public class DynamicRoleMappings : Profile
    {
        public DynamicRoleMappings()
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