namespace Core.AdminUI.Models.WorkflowEngineModels.ProcessTaskModels
{
    public class ProcessTaskResultModel<T>
    {
        public T? ModelData { get; set; }

        public Guid ProcessId { get; set; }

        public bool IsLastPage { get; set; } = true;
        public string? PagingToken { get; set; }
    }
}