using AutoMapper;
using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.HospitalPoliclinicQuotas;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Services.HandlerServices.HospitalPoliclinicQuotas
{
    public class HospitalPoliclinicQuotaQueryService : IHospitalPoliclinicQuotaQueryService
    {
        private readonly IRepository<HospitalPoliclinicQuota> _repository;
        private readonly IMapper _mapper;

        public HospitalPoliclinicQuotaQueryService(IRepository<HospitalPoliclinicQuota> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<InternalServiceResponse<IReadOnlyCollection<TResult>>> GetDatasAsync<TResult>(DBQueryOptions<HospitalPoliclinicQuota>? options = null)
            where TResult : IListResult
        {
            IReadOnlyCollection<HospitalPoliclinicQuota> result = await _repository.GetAllAsync();

            return InternalServiceResponse<IReadOnlyCollection<TResult>>.Success(_mapper.Map<IReadOnlyCollection<TResult>>(result));
        }

        public async Task<InternalServiceResponse<TResult>> GetDataAsync<TResult>(Guid id)
            where TResult : ISingleResult
        {
            DBQueryOptions<HospitalPoliclinicQuota> dBQueryOptions = new DBQueryOptions<HospitalPoliclinicQuota>();
            Expression<Func<HospitalPoliclinicQuota, bool>> filter = x => x.Id == id;
            dBQueryOptions.filter = filter;

            dBQueryOptions.thenIncludes = new Dictionary<Expression<Func<HospitalPoliclinicQuota, object>>, List<Expression<Func<object, object>>>>
            {
                {
                    x=>x.HospitalPoliclinic,
                    new List<Expression<Func<object, object>>>
                    {
                        y=>((HospitalPoliclinic)y).Hospital
                    }
                },
                {
                    x=>x.HospitalPoliclinic,
                    new List<Expression<Func<object, object>>>
                    {
                        y=>((HospitalPoliclinic)y).Policlinic
                    }
                },
                {
                    x=>x.QuotaType,
                    new List<Expression<Func<object, object>>>{}
                }
            };

            HospitalPoliclinicQuota result = await _repository.GetByIdAsync(dBQueryOptions);

            return InternalServiceResponse<TResult>.Success(_mapper.Map<TResult>(result));
        }

        public async Task<InternalServiceResponse<int>> GetDataCountAsync(DBQueryOptions<HospitalPoliclinicQuota>? options = null)
        {
            int result = await _repository.GetDataCountAsync(options);

            return InternalServiceResponse<int>.Success(result);
        }
    }
}