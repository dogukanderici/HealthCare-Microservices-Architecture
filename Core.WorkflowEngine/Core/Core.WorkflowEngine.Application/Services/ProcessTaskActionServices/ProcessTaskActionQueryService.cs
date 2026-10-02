using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessTaskActionServices;
using Core.WorkflowEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Services.ProcessTaskActionServices
{
    public class ProcessTaskActionQueryService : IProcessTaskActionQueryService
    {
        private readonly IRepository<ProcessTaskAction> _repository;
        private readonly IMapper _mapper;

        public ProcessTaskActionQueryService(IRepository<ProcessTaskAction> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<InternalServiceResponse<IReadOnlyCollection<TResult>>> GetDatasAsync<TResult>(DBQueryOptions<ProcessTaskAction>? options = null)
        {
            IReadOnlyCollection<ProcessTaskAction> repoResponse = await _repository.GetAllDataAsync(options);

            return InternalServiceResponse<IReadOnlyCollection<TResult>>.Success(_mapper.Map<IReadOnlyCollection<TResult>>(repoResponse));
        }

        public async Task<InternalServiceResponse<TResult>> GetDataByIdAsync<TResult>(Guid id)
        {
            DBQueryOptions<ProcessTaskAction> options = new DBQueryOptions<ProcessTaskAction>();
            options.filter = x => x.Id == id;

            ProcessTaskAction repoResponse = await _repository.GetDataAsync(options);

            return InternalServiceResponse<TResult>.Success(_mapper.Map<TResult>(repoResponse));
        }

        public async Task<InternalServiceResponse<IReadOnlyCollection<TResult>>> GetDatasByFilterAsync<TResult>(DBQueryOptions<ProcessTaskAction> options)
        {

            IReadOnlyCollection<ProcessTaskAction> repoResponse = await _repository.GetAllDataAsync(options);

            return InternalServiceResponse<IReadOnlyCollection<TResult>>.Success(_mapper.Map<IReadOnlyCollection<TResult>>(repoResponse));
        }

        public async Task<InternalServiceResponse<int>> GetDataCount(DBQueryOptions<ProcessTaskAction> options)
        {
            int repoResponse = await _repository.GetAllDataCountAsync(options);

            return InternalServiceResponse<int>.Success(repoResponse);
        }
    }
}