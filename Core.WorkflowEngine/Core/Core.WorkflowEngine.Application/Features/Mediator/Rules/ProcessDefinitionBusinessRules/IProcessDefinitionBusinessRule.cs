namespace Core.WorkflowEngine.Application.Features.Mediator.Rules.ProcessDefinitionBusinessRules
{
    public interface IProcessDefinitionBusinessRule
    {
        Task<bool> ExistingProcessDefinitionDataAsync(Guid id);
    }
}
