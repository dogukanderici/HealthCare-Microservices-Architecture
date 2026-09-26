using AutoMapper;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.RabbitMQCommands;
using Core.WorkflowEngine.Application.IntegrationServices.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mappings
{
    public class SyncUserEventMappings : Profile
    {
        public SyncUserEventMappings()
        {
            CreateMap<SyncUserEvent, CreateSyncUserEventCommand>().ReverseMap();
        }
    }
}