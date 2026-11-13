using Core.WebUI.Application.Dtos.WorkflowEngineDtos.DynamicPolicyDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebUI.Application.Interfaces.Services.WorkflowEngine
{
    public interface IDynamicPolicyService : IBaseService<DynamicPolicyResultDto, DynamicPolicyCreateDto, DynamicPolicyUpdateDto>
    {
    }
}
