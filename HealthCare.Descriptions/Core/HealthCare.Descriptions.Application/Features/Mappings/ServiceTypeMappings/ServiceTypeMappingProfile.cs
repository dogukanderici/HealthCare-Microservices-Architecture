using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediators.ServiceTypes.Commands;
using HealthCare.Descriptions.Application.Features.Mediators.ServiceTypes.Results;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mappings.ServiceTypeMappings
{
    public class ServiceTypeMappingProfile : Profile
    {
        public ServiceTypeMappingProfile()
        {
            CreateMap<ServicingType, GetServiceTypesQueryResult>().ReverseMap();
            CreateMap<ServicingType, GetServiceTypeByIdQueryResult>().ReverseMap();
            CreateMap<ServicingType, GetServiceTypesByFilterQueryResult>().ReverseMap();
            CreateMap<ServicingType, CreateServiceTypeCommand>().ReverseMap();
            CreateMap<ServicingType, UpdateServiceTypeCommand>().ReverseMap();
            CreateMap<ServicingType, RemoveServiceTypeCommand>().ReverseMap();
        }
    }
}