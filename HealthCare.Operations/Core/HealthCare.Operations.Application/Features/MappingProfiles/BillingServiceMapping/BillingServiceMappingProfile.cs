using AutoMapper;
using HealthCare.Operations.Application.Features.Mediator.Commands.BillingServiceCommands;
using HealthCare.Operations.Application.Features.Mediator.Results.BillingServiceResults;
using HealthCare.Operations.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Application.Features.MappingProfiles.BillingServiceMapping
{
    public class BillingServiceMappingProfile : Profile
    {
        public BillingServiceMappingProfile()
        {
            CreateMap<BillingService, GetBillingServicesQueryResult>().ReverseMap();
            CreateMap<BillingService, GetBillingServiceByIdQueryResult>().ReverseMap();
            CreateMap<BillingService, GetBillingServicesByFilterQueryResult>().ReverseMap();
            CreateMap<BillingService, CreateBillingServiceProperty>().ReverseMap();
            CreateMap<BillingService, UpdateBillingServiceProperty>().ReverseMap();
        }
    }
}
