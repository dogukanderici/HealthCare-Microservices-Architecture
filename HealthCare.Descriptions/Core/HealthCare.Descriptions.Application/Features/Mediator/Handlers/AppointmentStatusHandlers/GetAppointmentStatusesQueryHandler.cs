using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.AppointmentStatusQueries;
using HealthCare.Descriptions.Application.Features.Mediator.Results.AppointmentStatusResults;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.AppointmentStatusHandlers
{
    public class GetAppointmentStatusesQueryHandler : IRequestHandler<GetAppointmentStatusesQuery, List<GetAppointmentStatusesQueryResult>>
    {
        private readonly IRepository<AppointmentStatus> _repository;
        private readonly IMapper _mapper;

        public GetAppointmentStatusesQueryHandler(IRepository<AppointmentStatus> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAppointmentStatusesQueryResult>> Handle(GetAppointmentStatusesQuery request, CancellationToken cancellationToken)
        {
            List<AppointmentStatus> result = await _repository.GetDatasAsync();

            return _mapper.Map<List<GetAppointmentStatusesQueryResult>>(result);
        }
    }
}
