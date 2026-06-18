using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediator.Commands.ServiceCommands;
using HealthCare.Descriptions.Application.Features.Mediator.Results.ServiceResults;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.MappingProfiles.ServiceTypeMappings
{
    public class ServiceTypeProfileMapping : Profile
    {
        public ServiceTypeProfileMapping()
        {
            CreateMap<ServiceType, GetServiceTypesQueryResult>().ReverseMap();
            CreateMap<ServiceType, GetServiceTypeByIdQueryResult>().ReverseMap();
            CreateMap<ServiceType, GetServiceTypesByFilterQueryResult>().ReverseMap();
            CreateMap<ServiceType, CreateServiceTypeCommand>().ReverseMap();
            CreateMap<ServiceType, UpdateServiceTypeCommand>().ReverseMap();
        }
    }
}
