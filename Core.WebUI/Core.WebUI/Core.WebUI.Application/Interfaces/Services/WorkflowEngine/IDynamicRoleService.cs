using Core.WebUI.Application.Commons.Parameters;
using Core.WebUI.Application.Dtos.WorkflowEngineDtos.DynamicRoleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebUI.Application.Interfaces.Services.WorkflowEngine
{
    public interface IDynamicRoleService : IBaseService<DynamicRoleResultDto, DynamicRoleCreateDto, DynamicRoleUpdateDto>
    {
        Task<UIApiResponse<IReadOnlyCollection<DynamicRoleResultDto>>> GetListWithFilterAsync(DynamicRoleQueryDto filter);
    }
}