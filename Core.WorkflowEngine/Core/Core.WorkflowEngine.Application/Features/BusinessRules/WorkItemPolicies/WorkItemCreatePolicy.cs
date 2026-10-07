using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Extensions;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Helpers;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Wrapper;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.InstanceServices;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.IntegrationServices.RabbitMQServices;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessTaskActionServices;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessTaskService;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.WorkItemServices;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Features.BusinessRules.WorkItemPolicies
{
    public class WorkItemCreatePolicy : PolicyRule<WorkItem>, IWorkItemCreatePolicy
    {
        private readonly IWorkItemQueryService _queryService;
        private readonly IInstanceQueryService _instanceQueryService;
        private readonly ISyncUserEventQueryService _syncUserQueryService;
        private readonly IProcessTaskQueryService _taskQueryService;
        private readonly IProcessTaskActionQueryService _taskActionQueryService;

        public WorkItemCreatePolicy(IWorkItemQueryService queryService, IInstanceQueryService instanceQueryService, ISyncUserEventQueryService syncUserQueryService, IProcessTaskQueryService taskQueryService, IProcessTaskActionQueryService taskActionQueryService)
        {
            _queryService = queryService;
            _instanceQueryService = instanceQueryService;
            _syncUserQueryService = syncUserQueryService;
            _taskQueryService = taskQueryService;
            _taskActionQueryService = taskActionQueryService;
        }

        private async Task<InternalPolicyResponse> CheckUserIsActiveAsync(Guid userId)
        {
            InternalServiceResponse<int> serviceResponse = await _syncUserQueryService.CheckActiveUserAsync(userId);

            return serviceResponse.ExecuteServiceWithPolicy(x => x < 1, "User who will be assigned workitem is not active user!");
        }

        private async Task<InternalPolicyResponse> CheckInstanceIsExistAsync(Guid instanceId)
        {
            DBQueryOptions<Instance> dBQueryOptions = new DBQueryOptions<Instance>();
            dBQueryOptions.filter = x => x.Id == instanceId;

            InternalServiceResponse<int> serviceResponse = await _instanceQueryService.GetDataCount(dBQueryOptions);

            return serviceResponse.ExecuteServiceWithPolicy(x => x < 1, "Instance data not found for new workitem!");
        }

        private async Task<InternalPolicyResponse> CheckTaskIsExistAsync(Guid stepId)
        {
            DBQueryOptions<ProcessTask> dBQueryOptions = new DBQueryOptions<ProcessTask>();
            dBQueryOptions.filter = x => (
                (x.Id == stepId) &&
                (x.IsActive == true)
            );

            InternalServiceResponse<int> serviceResponse = await _taskQueryService.GetDataCount(dBQueryOptions);

            return serviceResponse.ExecuteServiceWithPolicy(x => x < 1, "Task data not found for new workitem!");
        }

        private async Task<InternalPolicyResponse> CheckTaskActionIsExistAsync(Guid selectedAction)
        {
            DBQueryOptions<ProcessTaskAction> dBQueryOptions = new DBQueryOptions<ProcessTaskAction>();
            dBQueryOptions.filter = x => (
                (x.Id == selectedAction) &&
                (x.IsActive == true)
            );

            InternalServiceResponse<int> serviceResponse = await _taskActionQueryService.GetDataCount(dBQueryOptions);

            return serviceResponse.ExecuteServiceWithPolicy(x => x < 1, "Task Action data not found for new workitem!");
        }

        protected override async Task<InternalPolicyResponse> CountExistingDataAsync(WorkItem entity)
        {
            DBQueryOptions<WorkItem> dBQueryOptions = new DBQueryOptions<WorkItem>();
            dBQueryOptions.filter = x => x.Id != entity.Id;

            InternalServiceResponse<int> serviceResponse = await _queryService.GetDataCount(dBQueryOptions);

            return serviceResponse.ExecuteServiceWithPolicy(x => x > 0, "WorkItem must be unique!");
        }

        public override async Task<InternalPolicyResponse> ExecuteAllRuleAsync(WorkItem entity)
        {
            PolicyResponseHelper policyHelper = new PolicyResponseHelper
            {
                ()=>CheckUserIsActiveAsync(entity.AssignedUserId),
                ()=>CheckInstanceIsExistAsync(entity.InstanceId),
                ()=>CheckTaskIsExistAsync(entity.StepId),
                ()=>CheckTaskActionIsExistAsync(entity.SelectedAction),
                ()=>CountExistingDataAsync(entity)
            };

            return await policyHelper.ExecutePolicyRulesAync();
        }
    }
}