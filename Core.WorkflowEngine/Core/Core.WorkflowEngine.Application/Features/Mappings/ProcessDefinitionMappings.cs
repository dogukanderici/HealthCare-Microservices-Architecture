using AutoMapper;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessDefinitionCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Results.InstanceResults;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessDefinitionResults;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskResults;
using Core.WorkflowEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mappings
{
    public class ProcessDefinitionMappings : Profile
    {
        public ProcessDefinitionMappings()
        {
            CreateMap<ProcessDefinition, GetProcessDefinitionsQueryResult>().ReverseMap();
            CreateMap<ProcessDefinition, GetProcessDefinitionByIdQueryResult>().ReverseMap();
            CreateMap<ProcessDefinition, GetProcessDefinitionsByFilterQueryResult>().ReverseMap();
            CreateMap<ProcessDefinition, GetProcessDefinitionCountQueryResult>().ReverseMap();
            CreateMap<ProcessDefinition, CreateProcessDefinitionCommand>().ReverseMap();
            CreateMap<ProcessDefinition, UpdateProcessDefinitionCommand>().ReverseMap();
            CreateMap<ProcessDefinition, DeleteProcessDefinitionCommand>().ReverseMap();

            CreateMap<ProcessDefinition, GetProcessTaskWithProcessResult>().ReverseMap();

            CreateMap<ProcessDefinition, GetInstanceWithProcessResult>().ReverseMap();
        }
    }
}