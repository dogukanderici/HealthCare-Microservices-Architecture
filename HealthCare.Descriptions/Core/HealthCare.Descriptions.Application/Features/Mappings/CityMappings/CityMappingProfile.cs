using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediators.Cities.Commands;
using HealthCare.Descriptions.Application.Features.Mediators.Cities.Results;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mappings.CityMappings
{
    public class CityMappingProfile : Profile
    {
        public CityMappingProfile()
        {
            CreateMap<City, GetCitiesQueryResult>().ReverseMap();
            CreateMap<City, GetCityByIdQueryResult>().ReverseMap();
            CreateMap<City, GetCitiesByFilterQueryResult>().ReverseMap();
            CreateMap<City, CreateCityCommand>().ReverseMap();
            CreateMap<City, UpdateCityCommand>().ReverseMap();
            CreateMap<City, RemoveCityCommand>().ReverseMap();
        }
    }
}