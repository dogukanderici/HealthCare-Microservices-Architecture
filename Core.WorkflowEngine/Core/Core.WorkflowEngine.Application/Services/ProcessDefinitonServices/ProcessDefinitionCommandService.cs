using Core.WorkflowEngine.Application.Commons.Constants;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.BusinessRules.ProcessDefinitionBusinessRules;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessDefitinionsServices;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Services.ProcessDefinitonServices
{
    public class ProcessDefinitionCommandService : IProcessDefinitionCommandService
    {
        private readonly IRepository<ProcessDefinition> _repository;
        private readonly IProcessDefinitionBusinessRule _businessRule;

        public ProcessDefinitionCommandService(IRepository<ProcessDefinition> repository, IProcessDefinitionBusinessRule businessRule)
        {
            _repository = repository;
            _businessRule = businessRule;
        }

        public async Task<InternalServiceResponse<ProcessDefinition>> GetDataForUpdateAsync(Guid id)
        {
            DBQueryOptions<ProcessDefinition> dBQueryOptions = new DBQueryOptions<ProcessDefinition>();
            dBQueryOptions.filter = x => (
                (x.Id == id) && (x.IsActive == true)
            );

            ProcessDefinition result = await _repository.GetDataAsync(dBQueryOptions);

            if (result == null)
                return InternalServiceResponse<ProcessDefinition>.Failure(InternalServiceResponseConstants.DataNotFound);

            return InternalServiceResponse<ProcessDefinition>.Success(result);
        }

        public async Task<InternalServiceResponse<Guid>> CreateAsync(ProcessDefinition entity, CancellationToken cancellationToken)
        {
            Guid result = await _repository.CreateDataAsync(entity);

            return InternalServiceResponse<Guid>.Success(result);
        }

        public async Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(ProcessDefinition entity)
        {
            // Veri yoksa true döner.
            bool ruleResult = await _businessRule.ExistingProcessDefinitionDataAsync(entity.Id);

            if (ruleResult)
            {
                return InternalServiceResponse<DateTimeOffset>.Failure(InternalServiceResponseConstants.NotValidBusinessRule);
            }

            DateTimeOffset updatedDate = await _repository.UpdateDataAsync(entity);

            return InternalServiceResponse<DateTimeOffset>.Success(updatedDate);
        }

        public async Task<InternalServiceResponse<bool>> DeleteAsync(Guid id)
        {
            DBQueryOptions<ProcessDefinition> dBQueryOptions = new DBQueryOptions<ProcessDefinition>();
            dBQueryOptions.filter = x => x.Id == id;

            ProcessDefinition existingData = await _repository.GetDataAsync(dBQueryOptions);

            if (existingData == null)
                return InternalServiceResponse<bool>.Failure("Data not found!");

            await _repository.DeleteDataAsync(existingData);

            return InternalServiceResponse<bool>.Success(true);
        }
    }
}