using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediators.Hospitals.Commands;
using HealthCare.Descriptions.Application.Features.Mediators.Hospitals.Results;
using HealthCare.Descriptions.Application.Features.Mediators.Hospitals.Results.Shared;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mappings.HospitalMappings
{
    public class HospitalMappingProfile : Profile
    {
        public HospitalMappingProfile()
        {
            CreateMap<Hospital, GetHospitalsQueryResult>().ReverseMap();
            CreateMap<Hospital, GetHospitalByIdQueryResult>().ReverseMap();
            CreateMap<Hospital, GetHospitalsByFilterQueryResult>().ReverseMap();
            CreateMap<Hospital, CreateHospitalCommand>().ReverseMap();
            CreateMap<Hospital, UpdateHospitalCommand>().ReverseMap();
            CreateMap<Hospital, RemoveHospitalCommand>().ReverseMap();

            //For Relations
            CreateMap<Hospital, HospitalSharedResult>().ReverseMap();
        }
    }
}