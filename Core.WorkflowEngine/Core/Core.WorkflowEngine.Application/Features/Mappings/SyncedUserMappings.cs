using AutoMapper;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.RabbitMQCommands.SyncedUserCommands;
using Core.WorkflowEngine.Application.IntegrationServices.RabbitMQ.UserEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mappings
{
    public class SyncedUserMappings : Profile
    {
        public SyncedUserMappings()
        {
            CreateMap<SyncUserEvent, SyncUserEvent>();

            CreateMap<SyncUserEvent, CreateSyncUserEventCommand>().ReverseMap();
            CreateMap<SyncUserEvent, UpdateSyncUserEventCommand>().ReverseMap();
        }
    }
}