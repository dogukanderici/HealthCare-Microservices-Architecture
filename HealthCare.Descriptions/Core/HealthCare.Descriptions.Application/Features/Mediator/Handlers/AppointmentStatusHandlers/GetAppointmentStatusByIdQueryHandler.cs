using AutoMapper;
using HealthCare.Configuration;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.AppointmentStatusQueries;
using HealthCare.Descriptions.Application.Features.Mediator.Results.AppointmentStatusResults;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.AppointmentStatusHandlers
{
    public class GetAppointmentStatusByIdQueryHandler : IRequestHandler<GetAppointmentStatusByIdQuery, GetAppointmentStatusByIdQueryResult>
    {
        private readonly IRepository<AppointmentStatus> _repository;
        private readonly IMapper _mapper;

        public GetAppointmentStatusByIdQueryHandler(IRepository<AppointmentStatus> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetAppointmentStatusByIdQueryResult> Handle(GetAppointmentStatusByIdQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<AppointmentStatus> dbQueryOptions = new DbQueryOptions<AppointmentStatus>();

            Expression<Func<AppointmentStatus, bool>> filter = apps => apps.Id == request.Id;
            dbQueryOptions.filter = filter;

            AppointmentStatus result = await _repository.GetDataAsync(dbQueryOptions);

            return _mapper.Map<GetAppointmentStatusByIdQueryResult>(result);
        }
    }
}
