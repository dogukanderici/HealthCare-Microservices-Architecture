using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessTaskService;
using Core.WorkflowEngine.Domain.Entities;
using System.Linq.Expressions;

namespace Core.WorkflowEngine.Application.Services.ProcessTaskServices
{
    public class ProcessTaskQueryService : IProcessTaskQueryService
    {
        private readonly IRepository<ProcessTask> _repository;
        private readonly IMapper _mapper;

        public ProcessTaskQueryService(IRepository<ProcessTask> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<InternalServiceResponse<IReadOnlyCollection<TResult>>> GetDatasAsync<TResult>(DBQueryOptions<ProcessTask>? options = null)
        {
            IReadOnlyCollection<ProcessTask> repoResponse = await _repository.GetAllDataAsync(options);

            return InternalServiceResponse<IReadOnlyCollection<TResult>>.Success(_mapper.Map<IReadOnlyCollection<TResult>>(repoResponse));
        }

        public async Task<InternalServiceResponse<TResult>> GetDataByIdAsync<TResult>(Guid id)
        {
            DBQueryOptions<ProcessTask> options = new DBQueryOptions<ProcessTask>();
            options.filter = x => x.Id == id;

            ProcessTask repoResponse = await _repository.GetDataAsync(options);

            return InternalServiceResponse<TResult>.Success(_mapper.Map<TResult>(repoResponse));
        }

        public async Task<InternalServiceResponse<TResult>> GetDataByProcessIdAsync<TResult>(Guid processId)
        {
            DBQueryOptions<ProcessTask> dBQueryOptions = new DBQueryOptions<ProcessTask>();
            dBQueryOptions.filter = x => (x.ProcessId == processId && x.IsStartStep == true);

            ProcessTask repoResponse = await _repository.GetDataAsync(dBQueryOptions);

            return InternalServiceResponse<TResult>.Success(_mapper.Map<TResult>(repoResponse));
        }

        public async Task<InternalServiceResponse<IReadOnlyCollection<TResult>>> GetDatasByFilterAsync<TResult>(DBQueryOptions<ProcessTask> options)
        {
            IReadOnlyCollection<ProcessTask> repoResponse = await _repository.GetAllDataAsync(options);

            return InternalServiceResponse<IReadOnlyCollection<TResult>>.Success(_mapper.Map<IReadOnlyCollection<TResult>>(repoResponse));
        }

        public async Task<InternalServiceResponse<int>> GetDataCount(DBQueryOptions<ProcessTask> options)
        {
            int repoResponse = await _repository.GetAllDataCountAsync(options);

            return InternalServiceResponse<int>.Success(repoResponse);
        }
    }
}