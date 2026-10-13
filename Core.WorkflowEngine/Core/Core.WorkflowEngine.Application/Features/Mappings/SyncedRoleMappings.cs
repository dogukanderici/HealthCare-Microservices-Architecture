using AutoMapper;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.RabbitMQCommands.SyncedRoleCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Results.SyncedRoleResults;
using Core.WorkflowEngine.Application.Features.Mediator.Results.SyncedRoleResults.Shared;
using Core.WorkflowEngine.Application.IntegrationServices.RabbitMQ.RoleEvent;
using Core.WorkflowEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mappings
{
    public class SyncedRoleMappings : Profile
    {
        public SyncedRoleMappings()
        {
            CreateMap<SyncedRole, GetSyncedRolesQueryResult>().ReverseMap();
            CreateMap<SyncedRole, GetSyncedRoleByIdQueryResult>().ReverseMap();

            CreateMap<SyncedRole, CreateSyncedRoleCommand>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();
            CreateMap<SyncedRole, UpdateSyncedRoleCommand>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();

            // For RabbitMQ Response
            CreateMap<SyncedRole, SyncRoleEvent>().ReverseMap();

            // For Realations
            CreateMap<SyncedRole, SharedSyncedRoleResult>().ReverseMap();
        }
    }
}
