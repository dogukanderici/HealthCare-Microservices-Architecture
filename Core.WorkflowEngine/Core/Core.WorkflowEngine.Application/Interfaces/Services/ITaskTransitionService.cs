using Core.WorkflowEngine.Application.Features.Mediator.Queries.ProcessTaskTransitionQueries;
using Core.WorkflowEngine.Application.ServiceDtos.ProcessTaskTransitionDtos;
using Core.WorkflowEngine.Configuration.Wrappers;
using Core.WorkflowEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Interfaces.Services
{
    public interface ITaskTransitionService : IBaseService<ProcessTaskTransition>
    {
        Task<InternalServiceResponse<ProcessTaskTransition>> GetDataByIdAsync(Guid id);
        Task<InternalServiceResponse<List<ProcessTaskTransition>>> GetDatasByFilterAsync(TaskTransitionFilterDto taskTransitionFilterDto);
    }
}
