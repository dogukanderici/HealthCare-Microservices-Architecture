using Core.WorkflowEngine.Application.Features.BusinessRules.InstancePolicies;
using Core.WorkflowEngine.Application.Features.BusinessRules.ProcessDefinitionPolicies;
using Core.WorkflowEngine.Application.Features.BusinessRules.ProcessTaskActionPolicies;
using Core.WorkflowEngine.Application.Features.BusinessRules.ProcessTaskPolicies;
using Core.WorkflowEngine.Application.Features.BusinessRules.ProcessTaskTransitionPolicies;
using Core.WorkflowEngine.Application.Features.BusinessRules.WorkItemPolicies;
using Microsoft.Extensions.DependencyInjection;

namespace Core.WorkflowEngine.Configuration.Extensions.ServiceExtensions
{
    public static class BusinessRulesRegistration
    {
        public static IServiceCollection AddBusinessRulesRegistration(this IServiceCollection services)
        {
            services.AddScoped(typeof(IInstanceCreatePolicy), typeof(InstanceCreatePolicy));
            services.AddScoped(typeof(IInstanceUpdatePolicy), typeof(InstanceUpdatePolicy));

            services.AddScoped(typeof(IProcessDefinitionCreatePolicy), typeof(ProcessDefinitionCreatePolicy));
            services.AddScoped(typeof(IProcessDefinitionUpdatePolicy), typeof(ProcessDefinitionUpdatePolicy));

            services.AddScoped(typeof(IProcessTaskCreatePolicy), typeof(ProcessTaskCreatePolicy));
            services.AddScoped(typeof(IProcessTaskUpdatePolicy), typeof(ProcessTaskUpdatePolicy));

            services.AddScoped(typeof(IProcessTaskActionCreatePolicy), typeof(ProcessTaskActionCreatePolicy));
            services.AddScoped(typeof(IProcessTaskActionUpdatePolicy), typeof(ProcessTaskActionUpdatePolicy));

            services.AddScoped(typeof(IProcessTaskTransitionCreatePolicy), typeof(ProcessTaskTransitionCreatePolicy));
            services.AddScoped(typeof(IProcessTaskTransitionUpdatePolicy), typeof(ProcessTaskTransitionUpdatePolicy));

            services.AddScoped(typeof(IWorkItemCreatePolicy), typeof(WorkItemCreatePolicy));
            services.AddScoped(typeof(IWorkItemUpdatePolicy), typeof(WorkItemUpdatePolicy));

            return services;
        }
    }
}