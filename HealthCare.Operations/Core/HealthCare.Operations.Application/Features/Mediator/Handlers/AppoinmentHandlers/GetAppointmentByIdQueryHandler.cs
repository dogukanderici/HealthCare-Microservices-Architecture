using AutoMapper;
using HealthCare.Operations.Application.Features.Mediator.Queries.AppoinmentQueries;
using HealthCare.Operations.Application.Features.Mediator.Results.AppoinmentResults;
using HealthCare.Operations.Application.Interfaces;
using HealthCare.Operations.Configurations;
using HealthCare.Operations.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Application.Features.Mediator.Handlers.AppoinmentHandlers
{
    public class GetAppointmentByIdQueryHandler : IRequestHandler<GetAppointmentByIdQuery, GetAppointmentByIdQueryResult>
    {

        private readonly IRepository<Appointment> _repository;
        private readonly IMapper _mapper;

        public GetAppointmentByIdQueryHandler(IRepository<Appointment> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetAppointmentByIdQueryResult> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<Appointment> queryOptions = new DbQueryOptions<Appointment>();

            Expression<Func<Appointment, bool>> filter = a => a.Id == request.Id;
            queryOptions.filter = filter;

            Appointment result = await _repository.GetDataAsync(queryOptions);

            return _mapper.Map<GetAppointmentByIdQueryResult>(result);
        }
    }
}
