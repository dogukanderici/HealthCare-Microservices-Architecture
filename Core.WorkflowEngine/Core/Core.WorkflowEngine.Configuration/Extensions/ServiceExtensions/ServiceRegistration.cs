using Core.WorkflowEngine.Application.Features.Commons.Utilities;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.Services;
using Core.WorkflowEngine.Application.Services;
using Core.WorkflowEngine.Persistence.Services.CurrentUserServices;
using Microsoft.Extensions.DependencyInjection;

namespace Core.WorkflowEngine.Configuration.Extensions.ServiceExtensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped(typeof(IInstanceService), typeof(InstanceService));
            services.AddScoped(typeof(IWorkItemService), typeof(WorkItemService));
            services.AddScoped(typeof(ITaskTransitionService), typeof(TaskTransitionService));
            services.AddScoped(typeof(ICurrentUserService), typeof(CurrentUserService));
            services.AddScoped(typeof(IProcessDefinitionService), typeof(ProcessDefinitionService));
            services.AddScoped(typeof(IProcessTaskService), typeof(ProcessTaskService));

            services.AddScoped(typeof(IDynamicPropertyJoiner), typeof(DynamicPropertyJoiner));

            return services;
        }
    }
}
