using AutoMapper;
using HealthCare.Operations.Application.Features.Mediator.Queries.AppoinmentQueries;
using HealthCare.Operations.Application.Interfaces;
using HealthCare.Operations.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Application.Features.Mediator.Handlers.AppoinmentHandlers
{
    public class GetAppointmentCountQueryHandler : IRequestHandler<GetAppointmentCountQuery, int>
    {

        private readonly IRepository<Appointment> _repository;

        public GetAppointmentCountQueryHandler(IRepository<Appointment> repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(GetAppointmentCountQuery request, CancellationToken cancellationToken)
        {
            int result = await _repository.GetDataCountAsync();

            return result;
        }
    }
}
