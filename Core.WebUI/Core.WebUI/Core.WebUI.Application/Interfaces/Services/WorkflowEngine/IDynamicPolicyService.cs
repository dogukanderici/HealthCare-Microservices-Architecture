using Core.WebUI.Application.Commons.Parameters;
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
        Task<UIApiResponse<IReadOnlyCollection<DynamicPolicyResultDto>>> GetListWithFilterAsync(DynamicPolicyQueryDto queryDto);
    }
}