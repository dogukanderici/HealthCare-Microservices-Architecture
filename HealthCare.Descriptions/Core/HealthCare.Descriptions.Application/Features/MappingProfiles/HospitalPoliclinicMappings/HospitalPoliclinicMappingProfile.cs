using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediator.Commands.HospitalPoliclinicCommands;
using HealthCare.Descriptions.Application.Features.Mediator.Results.HospitalPoliclinicResults;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.MappingProfiles.HospitalPoliclinicMappings
{
    public class HospitalPoliclinicMappingProfile : Profile
    {
        public HospitalPoliclinicMappingProfile()
        {
            CreateMap<HospitalPoliclinic, GetHospitalPoliclinicsQueryResult>().ReverseMap();
            CreateMap<HospitalPoliclinic, GetHospitalPoliclinicByIdQueryResult>().ReverseMap();
            CreateMap<HospitalPoliclinic, GetHospitalPoliclinicsByFilterQueryResult>().ReverseMap();
            CreateMap<HospitalPoliclinic, GetHospitalPoliclinicForHospitalPoliclinicQuotaQueryResult>().ReverseMap();
            CreateMap<HospitalPoliclinic, CreateHospitalPoliclinicCommand>().ReverseMap();
            CreateMap<HospitalPoliclinic, UpdateHospitalPoliclinicCommand>().ReverseMap();
        }
    }
}
