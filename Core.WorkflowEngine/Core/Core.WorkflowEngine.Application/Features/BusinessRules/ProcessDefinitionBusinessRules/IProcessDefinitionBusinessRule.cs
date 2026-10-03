namespace Core.WorkflowEngine.Application.Features.BusinessRules.ProcessDefinitionBusinessRules
{
    public interface IProcessDefinitionBusinessRule
    {
        Task<bool> ExistingProcessDefinitionDataAsync(Guid id);
    }
}
