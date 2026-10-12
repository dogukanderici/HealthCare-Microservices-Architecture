using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.DynamicPolicyServices;
using Core.WorkflowEngine.Domain.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Services.DynamicPolicyServices
{
    public class DynamicPolicyQueryService : IDynamicPolicyQueryService
    {
        private readonly IRepository<DynamicPolicy> _repository;
        private readonly IMapper _mapper;

        public DynamicPolicyQueryService(IRepository<DynamicPolicy> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<InternalServiceResponse<IReadOnlyCollection<TResult>>> GetDatasAsync<TResult>(DBQueryOptions<DynamicPolicy>? options = null)
        {
            IReadOnlyCollection<DynamicPolicy> repoResponse = await _repository.GetAllDataAsync(options);

            return InternalServiceResponse<IReadOnlyCollection<TResult>>.Success(_mapper.Map<IReadOnlyCollection<TResult>>(repoResponse));
        }

        public async Task<InternalServiceResponse<TResult>> GetDataByIdAsync<TResult>(Guid id)
        {
            DBQueryOptions<DynamicPolicy> options = new DBQueryOptions<DynamicPolicy>();
            options.filter = x => x.Id == id;

            DynamicPolicy repoResponse = await _repository.GetDataAsync(options);

            return InternalServiceResponse<TResult>.Success(_mapper.Map<TResult>(repoResponse));
        }

        public async Task<InternalServiceResponse<IReadOnlyCollection<TResult>>> GetDatasByFilterAsync<TResult>(DBQueryOptions<DynamicPolicy> options)
        {

            IReadOnlyCollection<DynamicPolicy> repoResponse = await _repository.GetAllDataAsync(options);

            return InternalServiceResponse<IReadOnlyCollection<TResult>>.Success(_mapper.Map<IReadOnlyCollection<TResult>>(repoResponse));
        }

        public async Task<InternalServiceResponse<int>> GetDataCount(DBQueryOptions<DynamicPolicy> options)
        {
            int repoResponse = await _repository.GetAllDataCountAsync(options);

            return InternalServiceResponse<int>.Success(repoResponse);
        }
    }
}