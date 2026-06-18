using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediator.Commands.PoliclinicCommands;
using HealthCare.Descriptions.Application.Features.Mediator.Results.PoliclinicResults;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.MappingProfiles.PoliclinicMapings
{
    public class PoliclinicMappingProfile : Profile
    {
        public PoliclinicMappingProfile()
        {
            CreateMap<Policlinic, GetPoliclinicsQueryResult>().ReverseMap();
            CreateMap<Policlinic, GetPoliclinicByIdQueryResult>().ReverseMap();
            CreateMap<Policlinic, GetPoliclinicsByFilterQueryResult>().ReverseMap();
            CreateMap<Policlinic, GetPoliclinicForHospitalPoliclinicQuotaQueryResult>().ReverseMap();
            CreateMap<Policlinic, CreatePoliclinicCommand>().ReverseMap();
            CreateMap<Policlinic, UpdatePoliclinicCommand>().ReverseMap();
        }
    }
}
