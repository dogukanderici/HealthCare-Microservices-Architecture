using AutoMapper;
using Core.WebUI.Application.Dtos.WorkflowEngineDtos.ProcessDefinitionDtos;

namespace Core.AdminUI.Mappings.ProcessDefinitionMappings
{
    public class ProcessDefinitionMappingProfile : Profile
    {
        public ProcessDefinitionMappingProfile()
        {
            CreateMap<ProcessDefinitionResultDto, ProcessDefinitionUpdateDto>().ReverseMap();
        }
    }
}
