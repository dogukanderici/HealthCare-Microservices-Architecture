using AutoMapper;
using Core.WebUI.Application.Dtos.WorkflowEngineDtos.DynamicPolicyDtos;

namespace Core.AdminUI.Mappings.DynamicPolicyMappings
{
    public class DynamicPolicyMappingProfile : Profile
    {
        public DynamicPolicyMappingProfile()
        {
            CreateMap<DynamicPolicyResultDto, DynamicPolicyUpdateDto>().ReverseMap();
        }
    }
}
