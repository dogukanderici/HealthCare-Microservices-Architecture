using AutoMapper;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskTransitionCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskTransitionQueries;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskTransitionResults;
using Core.WorkflowEngine.Application.Features.Mediator.Results.WorkflowExecutionResults;
using Core.WorkflowEngine.Application.ServiceDtos.ProcessTaskTransitionDtos;
using Core.WorkflowEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            CreateMap<GetProcessTaskTransitionsQuery, TaskTransitionFilterDto>().ReverseMap(); // Service Query Dto
            CreateMap<GetProcessTaskTransitionsByFilterQuery, TaskTransitionFilterDto>().ReverseMap(); // Service Query Dto
            CreateMap<ProcessTaskTransition, GetTransitionsByFilterQueryResult>().ReverseMap();
        }
    }
}