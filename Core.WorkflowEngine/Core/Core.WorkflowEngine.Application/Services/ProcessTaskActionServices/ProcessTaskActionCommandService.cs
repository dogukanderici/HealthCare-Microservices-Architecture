using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessTaskActionServices;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Services.ProcessTaskActionServices
{
    public class ProcessTaskActionCommandService : IProcessTaskActionCommandService
    {
        private readonly IRepository<ProcessTaskAction> _repository;

        public ProcessTaskActionCommandService(IRepository<ProcessTaskAction> repository)
        {
            _repository = repository;
        }

        public async Task<InternalServiceResponse<ProcessTaskAction>> GetDataForUpdateAsync(Guid id)
        {
            DBQueryOptions<ProcessTaskAction> options = new DBQueryOptions<ProcessTaskAction>();
            options.filter = x => x.Id == id;

            ProcessTaskAction repoResponse = await _repository.GetDataAsync(options);

            if (repoResponse == null)
                return InternalServiceResponse<ProcessTaskAction>.Failure("Data not found");

            return InternalServiceResponse<ProcessTaskAction>.Success(repoResponse);
        }

        public async Task<InternalServiceResponse<Guid>> CreateAsync(ProcessTaskAction entity, CancellationToken cancellationToken)
        {
            Guid repoResponse = await _repository.CreateDataAsync(entity);

            return InternalServiceResponse<Guid>.Success(repoResponse);
        }

        public async Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(ProcessTaskAction entity)
        {
            DateTimeOffset repoResponse = await _repository.UpdateDataAsync(entity);

            return InternalServiceResponse<DateTimeOffset>.Success(repoResponse);
        }

        public async Task<InternalServiceResponse<bool>> DeleteAsync(Guid id)
        {
            InternalServiceResponse<ProcessTaskAction> existedData = await GetDataForUpdateAsync(id);

            if (!existedData.IsSuccess)
                return InternalServiceResponse<bool>.Failure(existedData.ServiceMessage);

            await _repository.DeleteDataAsync(existedData.Data);

            return InternalServiceResponse<bool>.Success(true);
        }
    }
}