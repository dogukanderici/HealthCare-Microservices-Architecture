using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediators.Districts.Commands;
using HealthCare.Descriptions.Application.Features.Mediators.Districts.Results;
using HealthCare.Descriptions.Application.Features.Mediators.Districts.Results.Shared;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mappings.DistrictMappings
{
    public class DistrictMappingProfile : Profile
    {
        public DistrictMappingProfile()
        {
            CreateMap<District, GetDistrictsQueryResult>().ReverseMap();
            CreateMap<District, GetDistrictByIdQueryResult>().ReverseMap();
            CreateMap<District, GetDistrictsByFilterQueryResult>().ReverseMap();
            CreateMap<District, CreateDistrictCommand>().ReverseMap();
            CreateMap<District, UpdateDistrictCommand>().ReverseMap();
            CreateMap<District, RemoveDistrictCommand>().ReverseMap();

            //For Relations
            CreateMap<District, DistrictSharedResult>().ReverseMap();
        }
    }
}