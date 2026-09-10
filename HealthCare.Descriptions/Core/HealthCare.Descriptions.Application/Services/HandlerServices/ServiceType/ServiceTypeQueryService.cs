using AutoMapper;
using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.ServiceType;
using HealthCare.Descriptions.Domain.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Services.HandlerServices.ServiceTypes
{
    public class ServiceTypeQueryService : IServiceTypeQueryService
    {
        private readonly IRepository<ServicingType> _repository;
        private readonly IMapper _mapper;

        public ServiceTypeQueryService(IRepository<ServicingType> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<InternalServiceResponse<IReadOnlyCollection<TResult>>> GetDatasAsync<TResult>(DBQueryOptions<ServicingType>? options = null)
            where TResult : IListResult
        {
            IReadOnlyCollection<ServicingType> result = await _repository.GetAllAsync(options);

            return InternalServiceResponse<IReadOnlyCollection<TResult>>.Success(_mapper.Map<IReadOnlyCollection<TResult>>(result));
        }

        public async Task<InternalServiceResponse<TResult>> GetDataAsync<TResult>(Guid id)
            where TResult : ISingleResult
        {
            DBQueryOptions<ServicingType> dBQueryOptions = new DBQueryOptions<ServicingType>();
            Expression<Func<ServicingType, bool>> filter = x => x.Id == id;
            dBQueryOptions.filter = filter;

            ServicingType result = await _repository.GetByIdAsync(dBQueryOptions);

            return InternalServiceResponse<TResult>.Success(_mapper.Map<TResult>(result));
        }

        public async Task<InternalServiceResponse<int>> GetDataCountAsync(DBQueryOptions<ServicingType>? options = null)
        {
            int result = await _repository.GetDataCountAsync(options);

            return InternalServiceResponse<int>.Success(result);
        }
    }
}