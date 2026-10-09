using AutoMapper;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.RabbitMQCommands.SyncedRoleCommands;
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
            CreateMap<SyncedRole, CreateSyncedRoleCommand>().ReverseMap();
            CreateMap<SyncedRole, UpdateSyncedRoleCommand>().ReverseMap();

            // For RabbitMQ Response
            CreateMap<SyncedRole, SyncRoleEvent>();
        }
    }
}