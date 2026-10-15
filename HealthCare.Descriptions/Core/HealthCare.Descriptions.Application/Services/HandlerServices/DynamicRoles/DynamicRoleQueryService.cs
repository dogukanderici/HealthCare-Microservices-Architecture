using AutoMapper;
using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.DynamicRoles;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Services.HandlerServices.DynamicRoles
{
    public class DynamicRoleQueryService : IDynamicRoleQueryService
    {
        private readonly IRepository<DynamicRole> _repository;
        private readonly IMapper _mapper;

        public DynamicRoleQueryService(IRepository<DynamicRole> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<InternalServiceResponse<IReadOnlyCollection<TResult>>> GetDatasAsync<TResult>(DBQueryOptions<DynamicRole>? options = null)
            where TResult : IListResult
        {
            IReadOnlyCollection<DynamicRole> repoResponse = await _repository.GetAllAsync(options);

            return InternalServiceResponse<IReadOnlyCollection<TResult>>
                .Success(_mapper.Map<IReadOnlyCollection<TResult>>(repoResponse));
        }

        public async Task<InternalServiceResponse<TResult>> GetDataAsync<TResult>(Guid id)
            where TResult : ISingleResult
        {
            DBQueryOptions<DynamicRole> dBQueryOptions = new DBQueryOptions<DynamicRole>();
            dBQueryOptions.filter = x => x.Id == id;

            DynamicRole repoResponse = await _repository.GetByIdAsync(dBQueryOptions);

            return InternalServiceResponse<TResult>
                .Success(_mapper.Map<TResult>(repoResponse));
        }

        public async Task<InternalServiceResponse<int>> GetDataCountAsync(DBQueryOptions<DynamicRole>? options = null)
        {
            int repoResponse = await _repository.GetDataCountAsync(options);

            return InternalServiceResponse<int>
                .Success(repoResponse);
        }
    }
}