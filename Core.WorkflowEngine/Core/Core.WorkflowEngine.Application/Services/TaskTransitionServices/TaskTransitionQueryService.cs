using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.TaskTransitionServices;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Services.TaskTransitionServices
{
    public class TaskTransitionQueryService : ITaskTransitionQueryService
    {
        private readonly IRepository<ProcessTaskTransition> _repository;
        private readonly IMapper _mapper;

        public TaskTransitionQueryService(IRepository<ProcessTaskTransition> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<InternalServiceResponse<IReadOnlyCollection<TResult>>> GetDatasAsync<TResult>(DBQueryOptions<ProcessTaskTransition>? options = null)
        {
            IReadOnlyCollection<ProcessTaskTransition> repoResponse = await _repository.GetAllDataAsync(options);

            return InternalServiceResponse<IReadOnlyCollection<TResult>>.Success(_mapper.Map<IReadOnlyCollection<TResult>>(repoResponse));
        }

        public async Task<InternalServiceResponse<TResult>> GetDataByIdAsync<TResult>(Guid id)
        {
            DBQueryOptions<ProcessTaskTransition> options = new DBQueryOptions<ProcessTaskTransition>();
            options.filter = x => x.Id == id;

            ProcessTaskTransition repoResponse = await _repository.GetDataAsync(options);

            return InternalServiceResponse<TResult>.Success(_mapper.Map<TResult>(repoResponse));
        }

        public async Task<InternalServiceResponse<IReadOnlyCollection<TResult>>> GetDatasByFilterAsync<TResult>(DBQueryOptions<ProcessTaskTransition> options)
        {

            IReadOnlyCollection<ProcessTaskTransition> repoResponse = await _repository.GetAllDataAsync(options);

            return InternalServiceResponse<IReadOnlyCollection<TResult>>.Success(_mapper.Map<IReadOnlyCollection<TResult>>(repoResponse));
        }

        public async Task<InternalServiceResponse<int>> GetDataCount(DBQueryOptions<ProcessTaskTransition> options)
        {
            int repoResponse = await _repository.GetAllDataCountAsync(options);

            return InternalServiceResponse<int>.Success(repoResponse);
        }
    }
}