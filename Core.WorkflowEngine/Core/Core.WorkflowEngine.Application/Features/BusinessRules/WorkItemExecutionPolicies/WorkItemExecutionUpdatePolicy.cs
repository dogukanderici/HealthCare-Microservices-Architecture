using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Extensions;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Helpers;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Wrapper;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.WorkflowExecutionCommands;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.InstanceServices;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessTaskActionServices;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessTaskService;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.WorkItemServices;
using Core.WorkflowEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.BusinessRules.WorkItemExecutionPolicies
{
    public class WorkItemExecutionUpdatePolicy : PolicyRule<CommitWorkItemExecutionCommand>, IWorkItemExecutionUpdatePolicy
    {
        private readonly IInstanceQueryService _instanceQueryService;
        private readonly IWorkItemQueryService _workitemQueryService;
        private readonly IProcessTaskQueryService _taskQueryService;
        private readonly IProcessTaskActionQueryService _taskActionQueryService;

        public WorkItemExecutionUpdatePolicy(IInstanceQueryService instanceQueryService, IWorkItemQueryService workitemQueryService, IProcessTaskQueryService taskQueryService, IProcessTaskActionQueryService taskActionQueryService)
        {
            _instanceQueryService = instanceQueryService;
            _workitemQueryService = workitemQueryService;
            _taskQueryService = taskQueryService;
            _taskActionQueryService = taskActionQueryService;
        }

        private async Task<InternalPolicyResponse> CheckWorkItemIsExistAsync(Guid workitemId)
        {
            DBQueryOptions<WorkItem> dBQueryOptions = new DBQueryOptions<WorkItem>();
            dBQueryOptions.filter = x => x.Id == workitemId;

            InternalServiceResponse<int> serviceResponse = await _workitemQueryService.GetDataCount(dBQueryOptions);

            return serviceResponse.ExecuteServiceWithPolicy(x => x < 1, "Workitem not found!");
        }

        private async Task<InternalPolicyResponse> CheckWorkItemStatusAsync(Guid workitemId)
        {
            DBQueryOptions<WorkItem> dBQueryOptions = new DBQueryOptions<WorkItem>();
            dBQueryOptions.filter = x => (
                (x.Id == workitemId) &&
                (x.Status == 1)
            );

            InternalServiceResponse<int> serviceResponse = await _workitemQueryService.GetDataCount(dBQueryOptions);

            return serviceResponse.ExecuteServiceWithPolicy(x => x < 1, "Workitem status is not valid for update!");
        }

        private async Task<InternalPolicyResponse> CheckTaskIsExistAsync(Guid taskId)
        {
            DBQueryOptions<ProcessTask> dBQueryOptions = new DBQueryOptions<ProcessTask>();
            dBQueryOptions.filter = x => x.Id == taskId;

            InternalServiceResponse<int> serviceResponse = await _taskQueryService.GetDataCount(dBQueryOptions);

            return serviceResponse.ExecuteServiceWithPolicy(x => x < 1, "Task not found!");
        }

        private async Task<InternalPolicyResponse> CheckTaskActionIsExistAsync(Guid actionId)
        {
            DBQueryOptions<ProcessTaskAction> dBQueryOptions = new DBQueryOptions<ProcessTaskAction>();
            dBQueryOptions.filter = x => x.Id == actionId;

            InternalServiceResponse<int> serviceResponse = await _taskActionQueryService.GetDataCount(dBQueryOptions);

            return serviceResponse.ExecuteServiceWithPolicy(x => x < 1, "Task Action not found!");
        }

        protected override async Task<InternalPolicyResponse> CountExistingDataAsync(CommitWorkItemExecutionCommand entity)
        {
            DBQueryOptions<Instance> dBQueryOptions = new DBQueryOptions<Instance>();
            dBQueryOptions.filter = x => x.Id == entity.InstanceId;

            InternalServiceResponse<int> serviceResponse = await _instanceQueryService.GetDataCount(dBQueryOptions);

            return serviceResponse.ExecuteServiceWithPolicy(x => x < 1, "Instance Action not found!");
        }

        public override async Task<InternalPolicyResponse> ExecuteAllRuleAsync(CommitWorkItemExecutionCommand entity)
        {
            PolicyResponseHelper policyHelper = new PolicyResponseHelper
            {
                ()=>CheckWorkItemIsExistAsync(entity.WorkItemId),
                ()=>CheckWorkItemStatusAsync(entity.WorkItemId),
                ()=>CheckTaskIsExistAsync(entity.ProcessTaskId),
                ()=>CheckTaskActionIsExistAsync(entity.ActionId),
                ()=>CountExistingDataAsync(entity)
            };

            return await policyHelper.ExecutePolicyRulesAync();
        }
    }
}