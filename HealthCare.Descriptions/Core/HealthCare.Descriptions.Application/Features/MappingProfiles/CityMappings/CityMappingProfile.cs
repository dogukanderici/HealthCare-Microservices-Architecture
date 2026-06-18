using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediator.Commands.CityCommands;
using HealthCare.Descriptions.Application.Features.Mediator.Results.CityResults;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.MappingProfiles.CityMappings
{
    public class CityMappingProfile : Profile
    {
        public CityMappingProfile()
        {
            CreateMap<City, GetCitiesQueryResult>().ReverseMap();
            CreateMap<City, GetCityByIdQueryResult>().ReverseMap();
            CreateMap<City, GetCitiesByFilterQueryResult>().ReverseMap();
            CreateMap<City, GetCityForDistrictQueryResult>().ReverseMap();
            CreateMap<City, GetCityForHospitalQueryResult>().ReverseMap();
            CreateMap<City, GetCitiesQueryResult>().ReverseMap();
            CreateMap<City, CreateCityCommand>().ReverseMap();
            CreateMap<City, UpdateCityCommand>().ReverseMap();
        }
    }
}
