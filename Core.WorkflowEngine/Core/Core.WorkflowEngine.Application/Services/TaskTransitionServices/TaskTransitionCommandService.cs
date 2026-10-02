using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.TaskTransitionServices;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Services.TaskTransitionServices
{
    public class TaskTransitionCommandService : ITaskTransitionCommandService
    {
        private readonly IRepository<ProcessTaskTransition> _repository;

        public TaskTransitionCommandService(IRepository<ProcessTaskTransition> repository)
        {
            _repository = repository;
        }

        public async Task<InternalServiceResponse<ProcessTaskTransition>> GetDataForUpdateAsync(Guid id)
        {
            DBQueryOptions<ProcessTaskTransition> dBQueryOptions = new DBQueryOptions<ProcessTaskTransition>();
            dBQueryOptions.filter = x => x.Id == id;

            ProcessTaskTransition result = await _repository.GetDataAsync(dBQueryOptions);

            return InternalServiceResponse<ProcessTaskTransition>.Success(result);
        }

        public async Task<InternalServiceResponse<Guid>> CreateAsync(ProcessTaskTransition entity, CancellationToken cancellationToken)
        {
            Guid repoResponse = await _repository.CreateDataAsync(entity);

            return InternalServiceResponse<Guid>.Success(repoResponse);
        }

        public async Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(ProcessTaskTransition entity)
        {
            DateTimeOffset repoResponse = await _repository.UpdateDataAsync(entity);

            return InternalServiceResponse<DateTimeOffset>.Success(repoResponse);
        }

        public async Task<InternalServiceResponse<bool>> DeleteAsync(Guid id)
        {
            InternalServiceResponse<ProcessTaskTransition> existedData = await GetDataForUpdateAsync(id);

            if (!existedData.IsSuccess)
                return InternalServiceResponse<bool>.Failure(existedData.ServiceMessage);

            await _repository.DeleteDataAsync(existedData.Data);

            return InternalServiceResponse<bool>.Success(true);
        }
    }
}