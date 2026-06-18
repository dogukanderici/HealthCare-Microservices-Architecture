using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediator.Commands.QuotaTypeCommands;
using HealthCare.Descriptions.Application.Features.Mediator.Results.QuotaTypeResults;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.MappingProfiles.QuotaTypeMappings
{
    public class QuotaTypeMappingProfile : Profile
    {
        public QuotaTypeMappingProfile()
        {
            CreateMap<QuotaType, GetQuotaTypesQueryResult>().ReverseMap();
            CreateMap<QuotaType, GetQuotaTypeByIdQueryResult>().ReverseMap();
            CreateMap<QuotaType, GetQuotaTypesByFilterQueryResult>().ReverseMap();
            CreateMap<QuotaType, CreateQuotaTypeCommand>().ReverseMap();
            CreateMap<QuotaType, UpdateQuotaTypeCommand>().ReverseMap();
        }
    }
}
