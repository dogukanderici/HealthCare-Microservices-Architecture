using AutoMapper;
using HealthCare.Operations.Application.Features.Mediator.Commands.BillCommands;
using HealthCare.Operations.Application.Features.Mediator.Results.BillResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Application.Features.MappingProfiles.BillMappings
{
    public class BillMappingProfile : Profile
    {
        public BillMappingProfile()
        {
            CreateMap<BillMappingProfile, GetBillsQueryResult>().ReverseMap();
            CreateMap<BillMappingProfile, GetBillByIdQueryResult>().ReverseMap();
            CreateMap<BillMappingProfile, GetBillsByFilterQueryResult>().ReverseMap();
            CreateMap<BillMappingProfile, CreateBillCommand>().ReverseMap();
            CreateMap<BillMappingProfile, UpdateBillCommand>().ReverseMap();
        }
    }
}
