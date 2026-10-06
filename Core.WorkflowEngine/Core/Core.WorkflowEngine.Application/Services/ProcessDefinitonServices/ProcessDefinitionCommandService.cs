using Core.WorkflowEngine.Application.Commons.Constants;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Wrapper;
using Core.WorkflowEngine.Application.Features.BusinessRules.ProcessDefinitionPolicies;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessDefitinionsServices;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Services.ProcessDefinitonServices
{
    public class ProcessDefinitionCommandService : IProcessDefinitionCommandService
    {
        private readonly IRepository<ProcessDefinition> _repository;
        private readonly IProcessDefinitionCreatePolicy _createPolicy;
        private readonly IProcessDefinitionUpdatePolicy _updatePolicy;

        public ProcessDefinitionCommandService(IRepository<ProcessDefinition> repository, IProcessDefinitionCreatePolicy createPolicy, IProcessDefinitionUpdatePolicy updatePolicy)
        {
            _repository = repository;
            _createPolicy = createPolicy;
            _updatePolicy = updatePolicy;
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
            InternalPolicyResponse policyResponse = await _createPolicy.ExecuteAllRuleAsync(entity);

            if (!policyResponse.IsSuccess)
            {
                return InternalServiceResponse<Guid>.Failure(policyResponse.PolicyMessage);
            }

            Guid result = await _repository.CreateDataAsync(entity);

            return InternalServiceResponse<Guid>.Success(result);
        }

        public async Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(ProcessDefinition entity)
        {
            // Veri yoksa true döner.
            InternalPolicyResponse policyResponse = await _updatePolicy.ExecuteAllRuleAsync(entity);

            if (!policyResponse.IsSuccess)
            {
                return InternalServiceResponse<DateTimeOffset>.Failure(policyResponse.PolicyMessage);
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