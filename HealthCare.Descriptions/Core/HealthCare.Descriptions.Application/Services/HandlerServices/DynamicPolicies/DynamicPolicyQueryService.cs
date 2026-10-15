using AutoMapper;
using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.DynamicPolicies;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Services.HandlerServices.DynamicPolicies
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
            where TResult : IListResult
        {
            IReadOnlyCollection<DynamicPolicy> repoResponse = await _repository.GetAllAsync(options);

            return InternalServiceResponse<IReadOnlyCollection<TResult>>
                .Success(_mapper.Map<IReadOnlyCollection<TResult>>(repoResponse));
        }

        public async Task<InternalServiceResponse<TResult>> GetDataAsync<TResult>(Guid id)
            where TResult : ISingleResult
        {
            DBQueryOptions<DynamicPolicy> options = new DBQueryOptions<DynamicPolicy>();
            options.filter = x => x.Id == id;

            DynamicPolicy repoResponse = await _repository.GetByIdAsync(options);

            return InternalServiceResponse<TResult>.Success(_mapper.Map<TResult>(repoResponse));
        }

        public async Task<InternalServiceResponse<int>> GetDataCountAsync(DBQueryOptions<DynamicPolicy>? options = null)
        {
            int repoResponse = await _repository.GetDataCountAsync(options);

            return InternalServiceResponse<int>
                .Success(repoResponse);
        }
    }
}