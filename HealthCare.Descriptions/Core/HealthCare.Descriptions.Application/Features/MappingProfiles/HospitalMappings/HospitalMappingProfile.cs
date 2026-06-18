using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediator.Commands.HospitalCommands;
using HealthCare.Descriptions.Application.Features.Mediator.Results.HospitalResults;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.MappingProfiles.HospitalMappings
{
    public class HospitalMappingProfile : Profile
    {
        public HospitalMappingProfile()
        {
            CreateMap<Hospital, GetHospitalsQueryResult>().ReverseMap();
            CreateMap<Hospital, GetHospitalByIdQueryResult>().ReverseMap();
            CreateMap<Hospital, GetHospitalsByFilterQueryResult>().ReverseMap();
            CreateMap<Hospital, GetHospitalForHospitalPoliclinicQueryResult>().ReverseMap();
            CreateMap<Hospital, GetHospitalForHospitalPoliclinicQuotaQueryResult>().ReverseMap();
            CreateMap<Hospital, CreateHospitalCommand>().ReverseMap();
            CreateMap<Hospital, UpdateHospitalCommand>().ReverseMap();
        }
    }
}
