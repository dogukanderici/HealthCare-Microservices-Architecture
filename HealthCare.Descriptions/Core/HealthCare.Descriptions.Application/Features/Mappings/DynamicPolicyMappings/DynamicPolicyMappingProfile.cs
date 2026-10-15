using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediators.DynamicPolicies.Commands;
using HealthCare.Descriptions.Application.Features.Mediators.DynamicPolicies.Results;
using HealthCare.Descriptions.Application.Features.Mediators.DynamicPolicies.Results.Shared;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mappings.DynamicPolicyMappings
{
    public class DynamicPolicyMappingProfile : Profile
    {
        public DynamicPolicyMappingProfile()
        {
            CreateMap<DynamicPolicy, GetDynamicPoliciesQueryResult>().ReverseMap();
            CreateMap<DynamicPolicy, GetDynamicPolicyByIdQueryResult>().ReverseMap();
            CreateMap<DynamicPolicy, GetDynamicPoliciesByFilterQueryResult>().ReverseMap();
            CreateMap<DynamicPolicy, CreateDynamicPolicyCommand>().ReverseMap();
            CreateMap<DynamicPolicy, UpdateDynamicPolicyCommand>().ReverseMap();
            CreateMap<DynamicPolicy, DeleteDynamicPolicyCommand>().ReverseMap();

            // For Relations
            CreateMap<DynamicPolicy, SharedDynamicPolicyResult>().ReverseMap();
        }
    }
}