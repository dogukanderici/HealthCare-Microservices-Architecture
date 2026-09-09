using AutoMapper;
using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.Cities;
using HealthCare.Descriptions.Domain.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Services.HandlerServices.Cities
{
    public class CityQueryService : ICityQueryService
    {
        private readonly IRepository<City> _repository;
        private readonly IMapper _mapper;

        public CityQueryService(IRepository<City> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<InternalServiceResponse<IReadOnlyCollection<TResult>>> GetDatasAsync<TResult>(DBQueryOptions<City>? options = null)
            where TResult : IListResult
        {
            IReadOnlyCollection<City> result = await _repository.GetAllAsync(options);

            return InternalServiceResponse<IReadOnlyCollection<TResult>>.Success(_mapper.Map<IReadOnlyCollection<TResult>>(result));
        }

        public async Task<InternalServiceResponse<TResult>> GetDataAsync<TResult>(Guid id)
            where TResult : ISingleResult
        {
            DBQueryOptions<City> options = new DBQueryOptions<City>();
            Expression<Func<City, bool>> filter = x => x.Id == id;
            options.filter = filter;

            City result = await _repository.GetByIdAsync(options);

            return InternalServiceResponse<TResult>.Success(_mapper.Map<TResult>(result));
        }

        public async Task<InternalServiceResponse<int>> GetDataCountAsync(DBQueryOptions<City>? options = null)
        {
            int result = await _repository.GetDataCountAsync(options);

            return InternalServiceResponse<int>.Success(_mapper.Map<int>(result));
        }
    }
}