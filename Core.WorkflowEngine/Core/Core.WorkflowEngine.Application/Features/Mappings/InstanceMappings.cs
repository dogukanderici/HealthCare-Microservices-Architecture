using AutoMapper;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.InstanceCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.WorkflowExecutionCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Results.InboxResults;
using Core.WorkflowEngine.Application.Features.Mediator.Results.InstanceResults;
using Core.WorkflowEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.WorkflowEngine.Application.Features.Mappings
{
    public class InstanceMappings : Profile
    {
        public InstanceMappings()
        {
            CreateMap<Instance, GetInstancesQueryResult>().ReverseMap();
            CreateMap<Instance, GetInstanceByIdQueryResult>().ReverseMap();
            CreateMap<Instance, GetInstancesByFilterQueryResult>().ReverseMap();
            CreateMap<Instance, CreateInstanceCommand>().ReverseMap();
            CreateMap<Instance, UpdateInstanceCommand>().ReverseMap();
            CreateMap<Instance, DeleteInstanceCommand>().ReverseMap();

            CreateMap<Instance, CreateInstanceExecutionCommand>().ReverseMap();

            CreateMap<Instance, GetInboxWithInstanceResult>().ReverseMap();
        }
    }
}