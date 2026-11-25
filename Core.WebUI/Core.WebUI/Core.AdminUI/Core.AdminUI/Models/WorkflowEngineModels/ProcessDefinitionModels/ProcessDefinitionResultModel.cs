using Core.WebUI.Application.Interfaces;

namespace Core.AdminUI.Models.WorkflowEngineModels.ProcessDefinitionModels
{
    public class ProcessDefinitionResultModel<T>
    {
        public T? ModelData { get; set; }
        public string? PagingToken { get; set; }
        public bool? IsLastPage { get; set; } = true;
    }
}