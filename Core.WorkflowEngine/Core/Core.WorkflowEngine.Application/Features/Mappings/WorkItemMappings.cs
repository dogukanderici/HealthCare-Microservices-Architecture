using AutoMapper;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.WorkItemCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Results.WorkItemResults;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Features.Mappings
{
    public class WorkItemMappings : Profile
    {
        public WorkItemMappings()
        {
            CreateMap<WorkItem, GetWorkItemsQueryResult>().ReverseMap();
            CreateMap<WorkItem, GetWorkItemByIdQueryResult>().ReverseMap();
            CreateMap<WorkItem, GetWorkItemsByFilterQueryResult>().ReverseMap();
            CreateMap<WorkItem, CreateWorkItemCommand>().ReverseMap();
            CreateMap<WorkItem, UpdateWorkItemCommand>().ReverseMap();
            CreateMap<WorkItem, DeleteWorkItemCommand>().ReverseMap();
        }
    }
}