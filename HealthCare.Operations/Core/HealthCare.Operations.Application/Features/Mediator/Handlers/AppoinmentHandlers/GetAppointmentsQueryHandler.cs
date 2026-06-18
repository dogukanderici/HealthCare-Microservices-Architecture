using AutoMapper;
using HealthCare.Operations.Application.Features.Mediator.Queries.AppoinmentQueries;
using HealthCare.Operations.Application.Features.Mediator.Results.AppoinmentResults;
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
    public class GetAppointmentsQueryHandler : IRequestHandler<GetAppointmentsQuery, List<GetAppointmentsQueryResult>>
    {
        private readonly IRepository<Appointment> _repository;
        private readonly IMapper _mapper;

        public GetAppointmentsQueryHandler(IRepository<Appointment> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAppointmentsQueryResult>> Handle(GetAppointmentsQuery request, CancellationToken cancellationToken)
        {
            List<Appointment> result = await _repository.GetDatasAsync();

            return _mapper.Map<List<GetAppointmentsQueryResult>>(result);
        }
    }
}
