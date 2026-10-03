using Core.WorkflowEngine.Application.Features.Commons;
using Core.WorkflowEngine.Application.Features.BusinessRules.InstanceBusinessRules;
using Core.WorkflowEngine.Application.Features.BusinessRules.ProcessDefinitionBusinessRules;
using Core.WorkflowEngine.Application.Features.BusinessRules.ProcessTaskActionBusinessRules;
using Core.WorkflowEngine.Application.Features.BusinessRules.ProcessTaskBusinessRules;
using Core.WorkflowEngine.Application.Features.BusinessRules.ProcessTaskTransitionRules;
using Core.WorkflowEngine.Application.Features.BusinessRules.WorkItemBusinessRules;
using Core.WorkflowEngine.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Core.WorkflowEngine.Configuration.Extensions.ServiceExtensions
{
    public static class BusinessRulesRegistration
    {
        public static IServiceCollection AddBusinessRulesRegistration(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseBusinessRule<,>), typeof(BaseBusinessRule<,>));
            services.AddScoped(typeof(IInstanceBusinessRule), typeof(InstanceBusinessRule));
            services.AddScoped(typeof(IProcessDefinitionBusinessRule), typeof(ProcessDefinitionBusinessRule));
            services.AddScoped(typeof(IProcessTaskBusinessRule), typeof(ProcessTaskBusinessRule));
            services.AddScoped(typeof(IProcessTaskActionBusinessRule), typeof(ProcessTaskActionBusinessRule));
            services.AddScoped(typeof(ITaskTransitionBusinessRule), typeof(TaskTransitionBusinessRule));
            services.AddScoped(typeof(IWorkItemBusinessRule), typeof(WorkItemBusinessRule));

            return services;
        }
    }
}
