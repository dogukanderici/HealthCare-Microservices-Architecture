using AutoMapper;
using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.AppointmentStatutes;
using HealthCare.Descriptions.Domain.Abstracts;
using HealthCare.Descriptions.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace HealthCare.Descriptions.Application.Services.HandlerServices.AppointmentStatuses
{
    public class AppointmentStatusQueryService : IAppointmentStatusQueryService
    {

        private readonly IRepository<AppointmentStatus> _repository;
        private readonly IMapper _mapper;

        public AppointmentStatusQueryService(IRepository<AppointmentStatus> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<InternalServiceResponse<IReadOnlyCollection<TEntityResult>>> GetDatasAsync<TEntityResult>(DBQueryOptions<AppointmentStatus>? options = null)
            where TEntityResult : IListResult
        {
            IReadOnlyCollection<AppointmentStatus> results = await _repository.GetAllAsync(options);

            return InternalServiceResponse<IReadOnlyCollection<TEntityResult>>.Success(_mapper.Map<IReadOnlyCollection<TEntityResult>>(results));
        }

        public async Task<InternalServiceResponse<TEntityResult>> GetDataAsync<TEntityResult>(Guid id)
            where TEntityResult : ISingleResult
        {
            DBQueryOptions<AppointmentStatus> options = new DBQueryOptions<AppointmentStatus>();

            Expression<Func<AppointmentStatus, bool>> filter = x => x.Id == id;

            options.filter = filter;

            AppointmentStatus result = await _repository.GetByIdAsync(options);

            return InternalServiceResponse<TEntityResult>.Success(_mapper.Map<TEntityResult>(result));
        }

        public async Task<InternalServiceResponse<int>> GetDataCountAsync(DBQueryOptions<AppointmentStatus>? options = null)
        {
            int result = await _repository.GetDataCountAsync(options);

            return InternalServiceResponse<int>.Success(_mapper.Map<int>(result));
        }
    }
}