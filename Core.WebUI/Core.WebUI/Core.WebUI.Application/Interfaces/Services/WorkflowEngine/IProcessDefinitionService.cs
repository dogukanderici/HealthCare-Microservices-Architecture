using Core.WebUI.Application.Commons.Parameters;
using Core.WebUI.Application.Dtos.WorkflowEngineDtos.ProcessDefinitionDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebUI.Application.Interfaces.Services.WorkflowEngine
{
    public interface IProcessDefinitionService : IBaseService<ProcessDefinitionResultDto, ProcessDefinitionCreateDto, ProcessDefinitionUpdateDto>
    {
    }
}