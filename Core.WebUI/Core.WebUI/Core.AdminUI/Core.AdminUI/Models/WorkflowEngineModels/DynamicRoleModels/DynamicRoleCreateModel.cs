using Core.WebUI.Application.Dtos.IdentityDtos.RoleDtos;
using Core.WebUI.Application.Dtos.WorkflowEngineDtos.DynamicRoleDtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core.AdminUI.Models.WorkflowEngineModels.DynamicRoleModels
{
    public class DynamicRoleCreateModel
    {
        public DynamicRoleCreateDto ModelData { get; set; }

        public IReadOnlyCollection<SelectListItem> Roles { get; set; }


        public DynamicRoleCreateModel()
        {
            ModelData = new DynamicRoleCreateDto();
            Roles = [];
        }
    }
}