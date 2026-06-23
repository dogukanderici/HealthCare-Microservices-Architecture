using Core.WorkflowEngine.Application.Features.Commons;
using Core.WorkflowEngine.Application.Features.Mediator.Rules.InstanceBusinessRules;
using Core.WorkflowEngine.Application.Features.Mediator.Rules.ProcessDefinitionBusinessRules;
using Core.WorkflowEngine.Application.Features.Mediator.Rules.ProcessTaskBusinessRules;
using Core.WorkflowEngine.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Features.Configurations
{
    public static class BusinessRulesRegistration
    {
        public static IServiceCollection AddBusinessRulesRegistration(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseBusinessRule<,>), typeof(BaseBusinessRule<,>));
            services.AddScoped(typeof(IInstanceBusinessRule<,>), typeof(InstanceBusinessRule<,>));
            services.AddScoped(typeof(IProcessDefinitionBusinessRule), typeof(ProcessDefinitionBusinessRule));
            services.AddScoped(typeof(IProcessTaskBusinessRule), typeof(ProcessTaskBusinessRule));

            return services;
        }
    }
}