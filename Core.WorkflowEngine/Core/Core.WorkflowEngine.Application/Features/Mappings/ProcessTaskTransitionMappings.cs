using AutoMapper;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskTransitionCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskTransitionResults;
using Core.WorkflowEngine.Application.Features.Mediator.Results.WorkflowExecutionResults;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Features.Mappings
{
    public class ProcessTaskTransitionMappings : Profile
    {
        public ProcessTaskTransitionMappings()
        {
            CreateMap<ProcessTaskTransition, GetProcessTaskTransitionsQueryResult>().ReverseMap();
            CreateMap<ProcessTaskTransition, GetProcessTaskTransitionByIdQueryResult>().ReverseMap();
            CreateMap<ProcessTaskTransition, GetProcessTaskTransitionsByFilterQueryResult>().ReverseMap();
            CreateMap<ProcessTaskTransition, CreateProcessTaskTransitionCommand>().ReverseMap();
            CreateMap<ProcessTaskTransition, UpdateProcessTaskTransitionCommand>().ReverseMap();
            CreateMap<ProcessTaskTransition, DeleteProcessTaskTransitionCommand>().ReverseMap();

            CreateMap<ProcessTaskTransition, GetTransitionsByFilterQueryResult>().ReverseMap();

            CreateMap<ProcessTask, GetProcessTaskTransitionWithProcessTaskResult>().ReverseMap();
        }
    }
}