using AutoMapper;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.WorkflowExecutionCommands;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Features.Mappings
{
    public class WorkflowExecutionMappings : Profile
    {
        public WorkflowExecutionMappings()
        {
            CreateMap<WorkItem, CommitWorkItemExecutionCommand>().ReverseMap();
        }
    }
}