using AutoMapper;
using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.HospitalPoliclinics;
using HealthCare.Descriptions.Domain.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Services.HandlerServices.HospitalPoliclinics
{
    public class HospitalPoliclinicQueryService : IHospitalPoliclinicQueryService
    {
        private readonly IRepository<HospitalPoliclinic> _repository;
        private readonly IMapper _mapper;

        public HospitalPoliclinicQueryService(IRepository<HospitalPoliclinic> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<InternalServiceResponse<IReadOnlyCollection<TResult>>> GetDatasAsync<TResult>(DBQueryOptions<HospitalPoliclinic>? options = null)
            where TResult : IListResult
        {
            IReadOnlyCollection<HospitalPoliclinic> result = await _repository.GetAllAsync(options);

            return InternalServiceResponse<IReadOnlyCollection<TResult>>.Success(_mapper.Map<IReadOnlyCollection<TResult>>(result));
        }

        public async Task<InternalServiceResponse<TResult>> GetDataAsync<TResult>(Guid id)
            where TResult : ISingleResult
        {
            DBQueryOptions<HospitalPoliclinic> dBQueryOptions = new DBQueryOptions<HospitalPoliclinic>();
            Expression<Func<HospitalPoliclinic, bool>> filter = x => x.Id == id;
            dBQueryOptions.filter = filter;
            dBQueryOptions.thenIncludes = new Dictionary<Expression<Func<HospitalPoliclinic, object>>, List<Expression<Func<object, object>>>>
                {
                    {
                        x=>x.Hospital,
                        new List<Expression<Func<object, object>>>{ }
                    },

                    {
                        x=>x.Policlinic,
                        new List<Expression<Func<object, object>>>{ }
                    }
                };

            HospitalPoliclinic result = await _repository.GetByIdAsync(dBQueryOptions);

            return InternalServiceResponse<TResult>.Success(_mapper.Map<TResult>(result));
        }

        public async Task<InternalServiceResponse<int>> GetDataCountAsync(DBQueryOptions<HospitalPoliclinic>? options = null)
        {
            int result = await _repository.GetDataCountAsync(options);

            return InternalServiceResponse<int>.Success(result);
        }
    }
}