using Core.WorkflowEngine.Application.Features.Commons.Utilities;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.InstanceServices;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessDefitinionsServices;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessTaskService;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.TaskTransitionServices;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.WorkItemServices;
using Core.WorkflowEngine.Application.Services.InstanceServices;
using Core.WorkflowEngine.Application.Services.ProcessDefiniitonServices;
using Core.WorkflowEngine.Application.Services.ProcessDefinitonServices;
using Core.WorkflowEngine.Application.Services.ProcessTaskServices;
using Core.WorkflowEngine.Application.Services.TaskTransitionServices;
using Core.WorkflowEngine.Application.Services.WorkItemServices;
using Core.WorkflowEngine.Persistence.Services.CurrentUserServices;
using Microsoft.Extensions.DependencyInjection;

namespace Core.WorkflowEngine.Configuration.Extensions.ServiceExtensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped(typeof(IInstanceQueryService), typeof(InstanceQueryService));
            services.AddScoped(typeof(IInstanceCommandService), typeof(InstanceCommandService));

            services.AddScoped(typeof(IWorkItemQueryService), typeof(WorkItemQueryService));
            services.AddScoped(typeof(IWorkItemCommandService), typeof(WorkItemCommandService));

            services.AddScoped(typeof(ITaskTransitionQueryService), typeof(TaskTransitionQueryService));
            services.AddScoped(typeof(ITaskTransitionCommandService), typeof(TaskTransitionCommandService));

            services.AddScoped(typeof(IProcessDefinitionQueryService), typeof(ProcessDefinitionQueryService));
            services.AddScoped(typeof(IProcessDefinitionCommandService), typeof(ProcessDefinitionCommandService));

            services.AddScoped(typeof(IProcessTaskQueryService), typeof(ProcessTaskQueryService));
            services.AddScoped(typeof(IProcessTaskCommandService), typeof(ProcessTaskCommandService));


            services.AddScoped(typeof(ICurrentUserService), typeof(CurrentUserService));
            services.AddScoped(typeof(IDynamicPropertyJoiner), typeof(DynamicPropertyJoiner));

            return services;
        }
    }
}
