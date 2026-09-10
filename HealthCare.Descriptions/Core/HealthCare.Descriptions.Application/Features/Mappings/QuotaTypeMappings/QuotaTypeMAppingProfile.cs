using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediators.QuotaTypes.Commands;
using HealthCare.Descriptions.Application.Features.Mediators.QuotaTypes.Results;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mappings.QuotaTypeMappings
{
    public class QuotaTypeMAppingProfile : Profile
    {
        public QuotaTypeMAppingProfile()
        {
            CreateMap<QuotaType, GetQuotaTypesQueryResult>().ReverseMap();
            CreateMap<QuotaType, GetQuotaTypeByIdQueryResult>().ReverseMap();
            CreateMap<QuotaType, CreateQuotaTypeCommand>().ReverseMap();
            CreateMap<QuotaType, UpdateQuotaTypeCommand>().ReverseMap();
            CreateMap<QuotaType, RemoveQuotaTypeCommand>().ReverseMap();
        }
    }
}