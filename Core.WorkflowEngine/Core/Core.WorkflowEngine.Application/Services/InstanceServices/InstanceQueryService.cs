using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.InstanceServices;
using Core.WorkflowEngine.Application.ServiceDtos.InstanceDtos;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Services.InstanceServices
{
    public class InstanceQueryService : IInstanceQueryService
    {
        public Task<InternalServiceResponse<Instance>> GetDataByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<InternalServiceResponse<int>> GetDataCount(InstanceFilterDto filterDto)
        {
            throw new NotImplementedException();
        }

        public Task<InternalServiceResponse<IReadOnlyCollection<Instance>>> GetDatasAsync(InstanceFilterDto? filterDto = null)
        {
            throw new NotImplementedException();
        }

        public Task<InternalServiceResponse<IReadOnlyCollection<Instance>>> GetDatasByFilterAsync(InstanceFilterDto filterDto)
        {
            throw new NotImplementedException();
        }
    }
}