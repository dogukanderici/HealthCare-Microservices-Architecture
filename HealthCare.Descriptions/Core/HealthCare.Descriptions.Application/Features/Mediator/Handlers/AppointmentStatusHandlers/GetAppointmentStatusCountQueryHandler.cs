using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.AppointmentStatusQueries;
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
    public class GetAppointmentStatusCountQueryHandler : IRequestHandler<GetAppointmentStatusCountQuery, int>
    {
        private readonly IRepository<AppointmentStatus> _repository;

        public GetAppointmentStatusCountQueryHandler(IRepository<AppointmentStatus> repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(GetAppointmentStatusCountQuery request, CancellationToken cancellationToken)
        {
            int result = await _repository.GetAllDataCountAsync();

            return result;
        }
    }
}
