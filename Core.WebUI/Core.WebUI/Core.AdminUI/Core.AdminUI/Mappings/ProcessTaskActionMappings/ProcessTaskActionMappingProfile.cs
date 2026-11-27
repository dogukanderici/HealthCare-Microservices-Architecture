using AutoMapper;
using Core.WebUI.Application.Dtos.WorkflowEngineDtos.ProcessTaskActionDtos;

namespace Core.AdminUI.Mappings.ProcessTaskActionMappings
{
    public class ProcessTaskActionMappingProfile : Profile
    {
        public ProcessTaskActionMappingProfile()
        {
            CreateMap<ProcessTaskActionResultDto, ProcessTaskActionUpdateDto>().ReverseMap();
        }
    }
}
