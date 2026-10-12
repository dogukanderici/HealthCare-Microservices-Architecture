using AutoMapper;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.DynamicPolicyCommands;
using Core.WorkflowEngine.Application.Features.Mediator.Results.DynamicPolicyResults;
using Core.WorkflowEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Mappings
{
    public class DynamicPolicyMappings : Profile
    {
        public DynamicPolicyMappings()
        {
            CreateMap<DynamicPolicy, GetDynamicPoliciesQueryResult>().ReverseMap();
            CreateMap<DynamicPolicy, GetDynamicPolicyByIdQueryResult>().ReverseMap();
            CreateMap<DynamicPolicy, GetDynamicPoliciesByFilterQueryResult>().ReverseMap();
            CreateMap<DynamicPolicy, CreateDynamicPolicyCommand>().ReverseMap();
            CreateMap<DynamicPolicy, UpdateDynamicPolicyCommand>().ReverseMap();
            CreateMap<DynamicPolicy, DeleteDynamicPolicyCommand>().ReverseMap();
        }
    }
}