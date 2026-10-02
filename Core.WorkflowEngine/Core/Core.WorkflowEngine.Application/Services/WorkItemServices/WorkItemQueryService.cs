using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.WorkItemServices;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Services.WorkItemServices
{
    public class WorkItemQueryService : IWorkItemQueryService
    {
        private readonly IRepository<WorkItem> _repository;
        private readonly IMapper _mapper;

        public WorkItemQueryService(IRepository<WorkItem> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<InternalServiceResponse<IReadOnlyCollection<TResult>>> GetDatasAsync<TResult>(DBQueryOptions<WorkItem>? options = null)
        {
            IReadOnlyCollection<WorkItem> repoResponse = await _repository.GetAllDataAsync(options);

            return InternalServiceResponse<IReadOnlyCollection<TResult>>.Success(_mapper.Map<IReadOnlyCollection<TResult>>(repoResponse));
        }

        public async Task<InternalServiceResponse<TResult>> GetDataByIdAsync<TResult>(Guid id)
        {
            DBQueryOptions<WorkItem> options = new DBQueryOptions<WorkItem>();
            options.filter = x => x.Id == id;

            WorkItem repoResponse = await _repository.GetDataAsync(options);

            return InternalServiceResponse<TResult>.Success(_mapper.Map<TResult>(repoResponse));
        }

        public async Task<InternalServiceResponse<IReadOnlyCollection<TResult>>> GetDatasByFilterAsync<TResult>(DBQueryOptions<WorkItem> options)
        {
            IReadOnlyCollection<WorkItem> repoResponse = await _repository.GetAllDataAsync(options);

            return InternalServiceResponse<IReadOnlyCollection<TResult>>.Success(_mapper.Map<IReadOnlyCollection<TResult>>(repoResponse));
        }

        public async Task<InternalServiceResponse<int>> GetDataCount(DBQueryOptions<WorkItem> options)
        {
            int repoResponse = await _repository.GetAllDataCountAsync(options);

            return InternalServiceResponse<int>.Success(repoResponse);
        }

        public async Task<InternalServiceResponse<IReadOnlyCollection<TResult>>> GetWorkItemByFilterAsync<TResult>(DBQueryOptions<WorkItem> options)
        {
            IReadOnlyCollection<WorkItem> repoResponse = await _repository.GetAllDataAsync(options);

            return InternalServiceResponse<IReadOnlyCollection<TResult>>.Success(_mapper.Map<IReadOnlyCollection<TResult>>(repoResponse));
        }
    }
}