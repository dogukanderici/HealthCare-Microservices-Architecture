using AutoMapper;
using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.IntegrationServices.RabbitMQ.SyncedRoles;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Services.HandlerServices.IntegrationServices.RabbitMQ.SyncedRoles
{
    public class SyncedRoleQueryService : ISyncedRoleQueryService
    {
        private readonly IRepository<SyncedRole> _repository;
        private readonly IMapper _mapper;

        public SyncedRoleQueryService(IRepository<SyncedRole> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<InternalServiceResponse<IReadOnlyCollection<TResult>>> GetDatasAsync<TResult>(DBQueryOptions<SyncedRole>? options = null)
            where TResult : IListResult
        {
            IReadOnlyCollection<SyncedRole> repoResponse = await _repository.GetAllAsync(options);

            return InternalServiceResponse<IReadOnlyCollection<TResult>>
                .Success(_mapper.Map<IReadOnlyCollection<TResult>>(repoResponse));
        }

        public async Task<InternalServiceResponse<TResult>> GetDataAsync<TResult>(Guid id)
            where TResult : ISingleResult
        {
            DBQueryOptions<SyncedRole> options = new DBQueryOptions<SyncedRole>();
            options.filter = x => x.Id == id;

            SyncedRole repoResponse = await _repository.GetByIdAsync(options);

            return InternalServiceResponse<TResult>
                .Success(_mapper.Map<TResult>(repoResponse));
        }

        public async Task<InternalServiceResponse<int>> GetDataCountAsync(DBQueryOptions<SyncedRole>? options = null)
        {
            int repoResponse = await _repository.GetDataCountAsync(options);

            return InternalServiceResponse<int>.Success(repoResponse);
        }
    }
}