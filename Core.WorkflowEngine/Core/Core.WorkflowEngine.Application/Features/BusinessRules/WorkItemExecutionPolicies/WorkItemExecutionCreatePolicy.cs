using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Extensions;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Helpers;
using Core.WorkflowEngine.Application.Features.BusinessRules.Commons.Wrapper;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessDefitinionsServices;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessTaskService;
using Core.WorkflowEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.BusinessRules.WorkItemExecutionPolicies
{
    public class WorkItemExecutionCreatePolicy : PolicyRule<Instance>, IWorkItemExecutionCreatePolicy
    {
        private readonly IProcessDefinitionQueryService _processQueryService;
        private readonly IProcessTaskQueryService _taskQueryService;

        public WorkItemExecutionCreatePolicy(IProcessDefinitionQueryService processQueryService, IProcessTaskQueryService taskQueryService)
        {
            _processQueryService = processQueryService;
            _taskQueryService = taskQueryService;
        }

        private async Task<InternalPolicyResponse> CheckProcessIsActive(Guid processId)
        {
            DBQueryOptions<ProcessDefinition> dBQueryOptions = new DBQueryOptions<ProcessDefinition>();
            dBQueryOptions.filter = x => (
                (x.Id == processId) &&
                (x.IsActive == true)
            );

            InternalServiceResponse<int> serviceResponse = await _processQueryService.GetDataCount(dBQueryOptions);

            return serviceResponse.ExecuteServiceWithPolicy(x => x < 1, "Not found active process for given process id!");
        }

        protected override async Task<InternalPolicyResponse> CountExistingDataAsync(Instance entity)
        {
            DBQueryOptions<ProcessTask> dBQueryOptions = new DBQueryOptions<ProcessTask>();
            dBQueryOptions.filter = x => (
                (x.Id == entity.ProcessId) &&
                (x.IsStartStep == true) &&
                (x.IsActive == true)
            );

            InternalServiceResponse<int> serviceResponse = await _taskQueryService.GetDataCount(dBQueryOptions);

            return serviceResponse.ExecuteServiceWithPolicy(x => x < 1, "Not found starting step for given process!");
        }

        public override async Task<InternalPolicyResponse> ExecuteAllRuleAsync(Instance entity)
        {
            PolicyResponseHelper policyHelper = new PolicyResponseHelper
            {
                ()=>CheckProcessIsActive(entity.ProcessId),
                ()=>CountExistingDataAsync(entity)
            };

            return await policyHelper.ExecutePolicyRulesAync();
        }
    }
}