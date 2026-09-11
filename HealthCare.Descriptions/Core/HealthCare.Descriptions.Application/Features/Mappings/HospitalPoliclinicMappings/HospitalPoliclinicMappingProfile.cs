using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinics.Commands;
using HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinics.Results;
using HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinics.Results.Shared;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mappings.HospitalPoliclinicMappings
{
    public class HospitalPoliclinicMappingProfile : Profile
    {
        public HospitalPoliclinicMappingProfile()
        {
            CreateMap<HospitalPoliclinic, GetHospitalPoliclinicsQueryResult>().ReverseMap();
            CreateMap<HospitalPoliclinic, GetHospitalPoliclinicByIdQueryResult>().ReverseMap();
            CreateMap<HospitalPoliclinic, GetHospitalPoliclinicsByFilterQueryResult>().ReverseMap();
            CreateMap<HospitalPoliclinic, CreateHospitalPoliclinicCommand>().ReverseMap();
            CreateMap<HospitalPoliclinic, UpdateHospitalPoliclinicCommand>().ReverseMap();
            CreateMap<HospitalPoliclinic, RemoveHospitalPoliclinicCommand>().ReverseMap();

            //For Relations
            CreateMap<HospitalPoliclinic, HospitalPoliclinicSharedResult>().ReverseMap();
        }
    }
}