using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediator.Commands.DistrictCommands;
using HealthCare.Descriptions.Application.Features.Mediator.Results.DistrictResults;
using HealthCare.Descriptions.Domain.Entities;

namespace HealthCare.Descriptions.Application.Features.MappingProfiles.DistrictMappings
{
    public class DistrictMappingProfile : Profile
    {
        public DistrictMappingProfile()
        {
            CreateMap<District, GetDistrictsQueryResult>().ReverseMap();
            CreateMap<District, GetDistrictByIdQueryResult>().ReverseMap();
            CreateMap<District, GetDistrictsByFilterQueryResult>().ReverseMap();
            CreateMap<District, GetDistrictForHospitalQueryResult>().ReverseMap();
            CreateMap<District, CreateDistrictCommand>().ReverseMap();
            CreateMap<District, UpdateDistrictCommand>().ReverseMap();
        }
    }
}
