using Core.WebUI.Application.Dtos.WorkflowEngineDtos.DynamicPolicyDtos;

namespace Core.AdminUI.Models.WorkflowEngineModels.DynamicPolicyModels
{
    public class DynamicPolicyResultModel<T>
    {
        public bool Status { get; set; }
        public T? ModelData { get; set; }

        public DynamicPolicyQueryDto? QueryFilter { get; set; }
    }
}
