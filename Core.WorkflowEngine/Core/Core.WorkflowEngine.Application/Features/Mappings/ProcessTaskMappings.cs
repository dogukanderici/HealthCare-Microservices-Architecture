using AutoMapper;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskResults;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskTransitionResults;
using Core.WorkflowEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mappings
{
    public class ProcessTaskMappings : Profile
    {
        public ProcessTaskMappings()
        {
            CreateMap<ProcessTask, GetProcessTasksQueryResult>()
                .ForMember(dest => dest.Process, opt => opt.MapFrom(src => src.ProcessDefinition))
                .ReverseMap();
            CreateMap<ProcessTask, GetProcessTaskByIdQueryResult>().ReverseMap();
            CreateMap<ProcessTask, GetProcessTasksByFilterQueryResult>().ReverseMap();
            CreateMap<ProcessTask, CreateProcessTaskCommand>().ReverseMap();
            CreateMap<ProcessTask, UpdateProcessTaskCommand>().ReverseMap();
            CreateMap<ProcessTask, DeleteProcessTaskCommand>().ReverseMap();
        }
    }
}