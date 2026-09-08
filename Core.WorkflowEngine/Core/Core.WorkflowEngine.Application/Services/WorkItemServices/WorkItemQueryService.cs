using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.WorkItemServices;
using Core.WorkflowEngine.Application.ServiceDtos.WorkItemServiceDtos;
using Core.WorkflowEngine.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Core.WorkflowEngine.Application.Services.WorkItemServices
{
    public class WorkItemQueryService : IWorkItemQueryService
    {
        private readonly IRepository<WorkItem> _repository;
        private readonly ILogger<WorkItemQueryService> _logger;

        public WorkItemQueryService(IRepository<WorkItem> repository, ILogger<WorkItemQueryService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<InternalServiceResponse<IReadOnlyCollection<WorkItem>>> GetDatasAsync(WorkItemFilterDto? filterDto = null)
        {
            DBQueryOptions<WorkItem> dBQueryOptions = GetDbQueryOptions(filterDto);

            IReadOnlyCollection<WorkItem> result = await _repository.GetAllDataAsync(dBQueryOptions);

            return InternalServiceResponse<IReadOnlyCollection<WorkItem>>.Success(result);
        }

        public async Task<InternalServiceResponse<WorkItem>> GetDataByIdAsync(Guid id)
        {
            DBQueryOptions<WorkItem> dBQueryOptions = new DBQueryOptions<WorkItem>();

            Expression<Func<WorkItem, bool>> filter = x => x.Id == id;
            dBQueryOptions.filter = filter;

            WorkItem result = await _repository.GetDataAsync(dBQueryOptions);

            return InternalServiceResponse<WorkItem>.Success(result);
        }

        public async Task<InternalServiceResponse<IReadOnlyCollection<WorkItem>>> GetDatasByFilterAsync(WorkItemFilterDto filterDto)
        {
            DBQueryOptions<WorkItem> dBQueryOptions = GetDbQueryOptions(filterDto);

            IReadOnlyCollection<WorkItem> result = await _repository.GetAllDataAsync(dBQueryOptions);

            return InternalServiceResponse<IReadOnlyCollection<WorkItem>>.Success(result);
        }

        public async Task<InternalServiceResponse<IReadOnlyCollection<WorkItem>>> GetWorkItemByFilterAsync(DBQueryOptions<WorkItem> dBQueryOptions)
        {
            IReadOnlyCollection<WorkItem> result = await _repository.GetAllDataAsync(dBQueryOptions);

            return InternalServiceResponse<IReadOnlyCollection<WorkItem>>.Success(result);
        }

        public async Task<InternalServiceResponse<int>> GetDataCount(WorkItemFilterDto filterDto)
        {
            DBQueryOptions<WorkItem> dBQueryOptions = GetDbQueryOptions(filterDto, false);

            int result = await _repository.GetAllDataCountAsync(dBQueryOptions);

            return InternalServiceResponse<int>.Success(result);
        }

        private DBQueryOptions<WorkItem> GetDbQueryOptions(WorkItemFilterDto filterDto, bool isRelationalQuery = true)
        {
            DBQueryOptions<WorkItem> dBQueryOptions = new DBQueryOptions<WorkItem>();

            Expression<Func<WorkItem, bool>> filter = x => (
                (!filterDto.InstanceId.HasValue || x.InstanceId == filterDto.InstanceId) &&
                (!filterDto.WorkItemId.HasValue || x.Id == filterDto.WorkItemId) &&
                (!filterDto.AssignedUserId.HasValue || x.AssignedUserId == filterDto.AssignedUserId) &&
                (!filterDto.Status.HasValue || x.Status == filterDto.Status) &&
                (!filterDto.CreatedAt.HasValue || x.CreatedAt == filterDto.CreatedAt)
            );

            dBQueryOptions.filter = filter;

            return dBQueryOptions;
        }
    }
}