using Core.WorkflowEngine.Application.Commons.Constants;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.WorkItemServices;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Services.WorkItemServices
{
    public class WorkItemCommandService : IWorkItemCommandService
    {
        private readonly IRepository<WorkItem> _repository;

        public WorkItemCommandService(IRepository<WorkItem> repository)
        {
            _repository = repository;
        }

        public async Task<InternalServiceResponse<WorkItem>> GetDataForUpdateAsync(Guid id)
        {
            DBQueryOptions<WorkItem> dBQueryOptions = new DBQueryOptions<WorkItem>();
            dBQueryOptions.filter = x => x.Id == id;

            WorkItem repoResponse = await _repository.GetDataAsync(dBQueryOptions);

            if (repoResponse == null)
                return InternalServiceResponse<WorkItem>.Failure(InternalServiceResponseConstants.DataNotFound);

            return InternalServiceResponse<WorkItem>.Success(repoResponse);
        }

        public async Task<InternalServiceResponse<Guid>> CreateAsync(WorkItem entity, CancellationToken cancellationToken)
        {
            Guid repoResponse = await _repository.CreateDataAsync(entity);

            return InternalServiceResponse<Guid>.Success(repoResponse);
        }

        public async Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(WorkItem entity)
        {
            DateTimeOffset repoResponse = await _repository.UpdateDataAsync(entity);

            return InternalServiceResponse<DateTimeOffset>.Success(repoResponse);
        }

        public async Task<InternalServiceResponse<bool>> DeleteAsync(Guid id)
        {
            InternalServiceResponse<WorkItem> existedData = await GetDataForUpdateAsync(id);

            if (!existedData.IsSuccess)
                return InternalServiceResponse<bool>.Failure(existedData.ServiceMessage);

            await _repository.DeleteDataAsync(existedData.Data);

            return InternalServiceResponse<bool>.Success(true);
        }
    }
}