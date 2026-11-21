using Core.WebUI.Application.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core.AdminUI.Models.WorkflowEngineModels.DynamicRoleModels
{
    public class DynamicRoleResultModel<T> : IBaseResultModel<T>
    {
        public T ModelData { get; set; }

        public Guid PolicyId { get; set; }
    }
}