using Core.WorkflowEngine.Application.ServiceDtos.WorkItemServiceDtos;
using Core.WorkflowEngine.Configuration.Wrappers;
using Core.WorkflowEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Interfaces.Services
{
    public interface IWorkItemService : IBaseService<WorkItem>
    {
        Task<InternalServiceResponse<WorkItem>> GetWorkItemByIdAsync(WorkItemFilterDto workItemFilterDto);
    }
}
