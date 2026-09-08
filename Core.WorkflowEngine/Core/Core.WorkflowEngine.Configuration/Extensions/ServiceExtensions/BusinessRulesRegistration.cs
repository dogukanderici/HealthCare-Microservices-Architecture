using Core.WorkflowEngine.Application.Features.Commons;
using Core.WorkflowEngine.Application.Features.Mediator.Rules.InstanceBusinessRules;
using Core.WorkflowEngine.Application.Features.Mediator.Rules.ProcessDefinitionBusinessRules;
using Core.WorkflowEngine.Application.Features.Mediator.Rules.ProcessTaskActionBusinessRules;
using Core.WorkflowEngine.Application.Features.Mediator.Rules.ProcessTaskBusinessRules;
using Core.WorkflowEngine.Application.Features.Mediator.Rules.ProcessTaskTransitionRules;
using Core.WorkflowEngine.Application.Features.Mediator.Rules.WorkItemBusinessRules;
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
