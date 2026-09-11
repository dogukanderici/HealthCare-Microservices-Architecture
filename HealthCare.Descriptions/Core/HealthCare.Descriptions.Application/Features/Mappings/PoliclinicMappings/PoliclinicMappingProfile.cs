using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediators.Policlinics.Commands;
using HealthCare.Descriptions.Application.Features.Mediators.Policlinics.Results;
using HealthCare.Descriptions.Application.Features.Mediators.Policlinics.Results.Shared;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mappings.PoliclinicMappings
{
    public class PoliclinicMappingProfile : Profile
    {
        public PoliclinicMappingProfile()
        {
            CreateMap<Policlinic, GetPoliclinicsQueryResult>().ReverseMap();
            CreateMap<Policlinic, GetPoliclinicByIdQueryResult>().ReverseMap();
            CreateMap<Policlinic, GetPoliclinicsByFilterQueryResult>().ReverseMap();
            CreateMap<Policlinic, CreatePoliclinicCommand>().ReverseMap();
            CreateMap<Policlinic, UpdatePoliclinicCommand>().ReverseMap();
            CreateMap<Policlinic, RemovePoliclinicCommand>().ReverseMap();

            //For Relations
            CreateMap<Policlinic, PoliclinicSharedResult>().ReverseMap();
        }
    }
}