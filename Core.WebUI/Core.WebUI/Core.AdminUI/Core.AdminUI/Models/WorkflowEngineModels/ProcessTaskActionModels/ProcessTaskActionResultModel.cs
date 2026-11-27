using Core.AdminUI.Models.Commons.Interfaces;

namespace Core.AdminUI.Models.WorkflowEngineModels.ProcessTaskActionModels
{
    public class ProcessTaskActionResultModel<T> : IBaseModel
    {
        public T? ModelData { get; set; }
        public Guid ProcessTaskId { get; set; }
        public string? PagingToken { get; set; }
        public bool? IsLastPage { get; set; } = true;
    }
}