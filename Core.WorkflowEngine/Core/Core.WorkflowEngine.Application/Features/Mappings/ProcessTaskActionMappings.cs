using AutoMapper;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.ProcessTaskActionCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Results.ProcessTaskActionResults;
using Core.WorkflowEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mappings
{
    public class ProcessTaskActionMappings : Profile
    {
        public ProcessTaskActionMappings()
        {
            CreateMap<ProcessTaskAction, GetProcessTaskActionsQueryResult>().ReverseMap();
            CreateMap<ProcessTaskAction, GetProcessTaskActionByIdQueryResult>().ReverseMap();
            CreateMap<ProcessTaskAction, GetProcessTaskActionsByFilterQueryResult>().ReverseMap();
            CreateMap<ProcessTaskAction, CreateProcessTaskActionCommand>().ReverseMap();
            CreateMap<ProcessTaskAction, UpdateProcessTaskActionCommand>().ReverseMap();
            CreateMap<ProcessTaskAction, DeleteProcessTaskActionCommand>().ReverseMap();
        }
    }
}