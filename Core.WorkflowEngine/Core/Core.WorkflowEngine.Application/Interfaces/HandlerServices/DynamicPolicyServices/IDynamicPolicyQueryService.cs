using Core.WorkflowEngine.Application.Features.BusinessRules;
using Core.WorkflowEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Interfaces.HandlerServices.DynamicPolicyServices
{
    public interface IDynamicPolicyQueryService : IBaseQueryService<DynamicPolicy>
    {
    }
}