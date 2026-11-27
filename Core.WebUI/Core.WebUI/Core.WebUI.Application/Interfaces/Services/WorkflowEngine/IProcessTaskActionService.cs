using Core.WebUI.Application.Dtos.WorkflowEngineDtos.ProcessTaskActionDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebUI.Application.Interfaces.Services.WorkflowEngine
{
    public interface IProcessTaskActionService : IBaseService<ProcessTaskActionResultDto, ProcessTaskActionCreateDto, ProcessTaskActionUpdateDto>
    {
    }
}
