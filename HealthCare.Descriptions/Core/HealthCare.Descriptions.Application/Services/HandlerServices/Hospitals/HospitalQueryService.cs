using AutoMapper;
using AutoMapper.Execution;
using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.Hospitals;
using HealthCare.Descriptions.Domain.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Services.HandlerServices.Hospitals
{
    public class HospitalQueryService : IHospitalQueryService
    {
        private readonly IRepository<Hospital> _repository;
        private readonly IMapper _mapper;

        public HospitalQueryService(IRepository<Hospital> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<InternalServiceResponse<IReadOnlyCollection<TResult>>> GetDatasAsync<TResult>(DBQueryOptions<Hospital>? options = null)
            where TResult : IListResult
        {
            IReadOnlyCollection<Hospital> result = await _repository.GetAllAsync(options);

            return InternalServiceResponse<IReadOnlyCollection<TResult>>.Success(_mapper.Map<IReadOnlyCollection<TResult>>(result));
        }

        public async Task<InternalServiceResponse<TResult>> GetDataAsync<TResult>(Guid id)
            where TResult : ISingleResult
        {
            DBQueryOptions<Hospital> dBQueryOptions = new DBQueryOptions<Hospital>();
            Expression<Func<Hospital, bool>> filter = x => x.Id == id;
            dBQueryOptions.filter = filter;

            dBQueryOptions.thenIncludes = new Dictionary<Expression<Func<Hospital, object>>, List<Expression<Func<object, object>>>>()
                {
                    {
                        x=>x.City,
                        new List<Expression<Func<object, object>>>
                        {
                            y=>((City)y).Districts
                        }
                    }
                };

            Hospital result = await _repository.GetByIdAsync(dBQueryOptions);

            return InternalServiceResponse<TResult>.Success(_mapper.Map<TResult>(result));
        }

        public async Task<InternalServiceResponse<int>> GetDataCountAsync(DBQueryOptions<Hospital>? options = null)
        {
            int result = await _repository.GetDataCountAsync(options);

            return InternalServiceResponse<int>.Success(result);
        }
    }
}