using Core.WorkflowEngine.Application.Interfaces.Services;
using Core.WorkflowEngine.Application.ServiceDtos.InstanceDtos;
using Core.WorkflowEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Interfaces.HandlerServices.InstanceServices
{
    public interface IInstanceQueryService : IBaseQueryService<Instance, InstanceFilterDto>
    {
    }
}