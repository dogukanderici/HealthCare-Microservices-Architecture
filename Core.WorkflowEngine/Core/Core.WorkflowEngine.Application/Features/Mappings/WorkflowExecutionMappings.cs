using AutoMapper;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.WorkflowExecutionCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.WorkflowExecutionQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.WorkflowExecutionResults;
using Core.WorkflowEngine.Application.ServiceDtos.ProcessTaskTransitionDtos;
using Core.WorkflowEngine.Application.ServiceDtos.WorkItemServiceDtos;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Features.Mappings
{
    public class WorkflowExecutionMappings : Profile
    {
        public WorkflowExecutionMappings()
        {
            CreateMap<TaskTransitionFilterDto, GetTransitionsByFilterQuery>().ReverseMap();
            CreateMap<WorkItemFilterDto, CommitWorkItemExecutionCommand>().ReverseMap();
            CreateMap<TaskTransitionFilterDto, CommitWorkItemExecutionCommand>().ReverseMap();
            CreateMap<WorkItem, CommitWorkItemExecutionCommand>().ReverseMap();
        }
    }
}