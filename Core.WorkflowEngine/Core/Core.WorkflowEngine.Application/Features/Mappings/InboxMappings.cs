using AutoMapper;
using Core.WorkflowEngine.Application.Features.Mediator.Results.InboxResults;
using Core.WorkflowEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mappings
{
    public class InboxMappings : Profile
    {
        public InboxMappings()
        {
            CreateMap<WorkItem, GetInboxByUserIdQueryResult>().ReverseMap();
        }
    }
}