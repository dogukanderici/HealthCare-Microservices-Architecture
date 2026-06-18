using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediator.Commands.HospitalPoliclinicQuotaCommands;
using HealthCare.Descriptions.Application.Features.Mediator.Results.HospitalPoliclinicQuotaResults;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.MappingProfiles.HospitalPoliclinicQuotaMappings
{
    public class HospitalPoliclinicQuotaMappingProfile : Profile
    {
        public HospitalPoliclinicQuotaMappingProfile()
        {
            CreateMap<HospitalPoliclinicQuota, GetHospitalPoliclinicQuotasQueryResult>().ReverseMap();
            CreateMap<HospitalPoliclinicQuota, GetHospitalPoliclinicQuotaByIdQueryResult>().ReverseMap();
            CreateMap<HospitalPoliclinicQuota, GetHospitalPoliclinicQuotasByFilterQueryResult>().ReverseMap();
            CreateMap<HospitalPoliclinicQuota, CreateHospitalPoliclinicQuotaCommand>().ReverseMap();
            CreateMap<HospitalPoliclinicQuota, UpdateHospitalPoliclinicQuotaCommand>().ReverseMap();
        }
    }
}
