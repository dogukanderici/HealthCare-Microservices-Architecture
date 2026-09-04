using AutoMapper;
using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices;
using HealthCare.Descriptions.Domain.Entities;

namespace HealthCare.Descriptions.Application.Services.HandlerServices
{
    public class AppointmentStatusService<TEntityResult> : IAppointmentStatusService<AppointmentStatus, TEntityResult>
        where TEntityResult : class
    {

        private readonly IRepository<AppointmentStatus> _repository;
        private readonly IMapper _mapper;

        public AppointmentStatusService(IRepository<AppointmentStatus> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyCollection<TEntityResult>> GetDatasAsync(DBQueryOptions<AppointmentStatus>? options = null)
        {
            ICollection<AppointmentStatus> results = await _repository.GetAllAsync(options);

            return _mapper.Map<IReadOnlyCollection<TEntityResult>>(results);
        }

        public async Task<TEntityResult> GetDataAsync(DBQueryOptions<AppointmentStatus>? options = null)
        {
            AppointmentStatus result = await _repository.GetByIdAsync(options);

            return _mapper.Map<TEntityResult>(result);
        }

        public async Task<int> GetDataCountAsync(DBQueryOptions<AppointmentStatus>? options = null)
        {
            return await _repository.GetDataCountAsync(options);
        }
    }
}
