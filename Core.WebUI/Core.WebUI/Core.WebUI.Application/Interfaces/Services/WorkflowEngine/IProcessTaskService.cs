using Core.WebUI.Application.Dtos.WorkflowEngineDtos.ProcessTaskDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebUI.Application.Interfaces.Services.WorkflowEngine
{
    public interface IProcessTaskService : IBaseService<ProcessTaskResultDto, ProcessTaskCreateDto, ProcessTaskUpdateDto>
    {
    }
}
