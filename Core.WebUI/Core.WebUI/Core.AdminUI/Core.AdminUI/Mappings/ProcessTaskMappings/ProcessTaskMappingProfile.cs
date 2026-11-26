using AutoMapper;
using Core.WebUI.Application.Dtos.WorkflowEngineDtos.ProcessTaskDtos;

namespace Core.AdminUI.Mappings.ProcessTaskMappings
{
    public class ProcessTaskMappingProfile : Profile
    {
        public ProcessTaskMappingProfile()
        {
            CreateMap<ProcessTaskResultDto, ProcessTaskUpdateDto>().ReverseMap();
        }
    }
}
