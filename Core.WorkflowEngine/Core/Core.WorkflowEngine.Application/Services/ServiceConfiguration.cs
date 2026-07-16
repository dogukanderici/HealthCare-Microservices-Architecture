using Core.WorkflowEngine.Application.Features.Commons.Utilities;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Services
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddServiceRegistartion(this IServiceCollection services)
        {
            services.AddScoped(typeof(IInstanceService), typeof(InstanceService));
            services.AddScoped(typeof(IWorkItemService), typeof(WorkItemService));
            services.AddScoped(typeof(ITaskTransitionService), typeof(TaskTransitionService));
            services.AddScoped(typeof(ICurrentUserService), typeof(CurrentUserService));
            services.AddScoped(typeof(IProcessDefinitionService), typeof(ProcessDefinitionService));

            services.AddScoped(typeof(IDynamicPropertyJoiner), typeof(DynamicPropertyJoiner));

            return services;
        }
    }
}