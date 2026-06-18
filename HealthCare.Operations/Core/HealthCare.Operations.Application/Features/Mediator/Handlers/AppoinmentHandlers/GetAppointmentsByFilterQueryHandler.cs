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
    public class GetAppointmentsByFilterQueryHandler : IRequestHandler<GetAppointmentsByFilterQuery, List<GetAppointmentsByFilterQueryResult>>
    {

        private readonly IRepository<Appointment> _repository;
        private readonly IMapper _mapper;

        public GetAppointmentsByFilterQueryHandler(IRepository<Appointment> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAppointmentsByFilterQueryResult>> Handle(GetAppointmentsByFilterQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<Appointment> dbQueryOptions = new DbQueryOptions<Appointment>();

            Expression<Func<Appointment, bool>> filter = a => (
                (String.IsNullOrEmpty(request.NameSurname) || a.NameSurname == request.NameSurname)
                &&
                (!request.Nationality.HasValue || a.Nationality == request.Nationality)
                &&
                (String.IsNullOrEmpty(request.IDNumber) || a.IDNumber == request.IDNumber)
                &&
                (String.IsNullOrEmpty(request.Phone) || a.Email == request.Phone)
                &&
                (String.IsNullOrEmpty(request.Email) || a.Email == request.Email)
                &&
                (!request.HospitalId.HasValue || a.HospitalId == request.HospitalId)
                &&
                (!request.PoliclinicId.HasValue || a.PoliclinicId == request.PoliclinicId)
                &&
                (!request.City.HasValue || a.City == request.City)
                &&
                (!request.District.HasValue || a.District == request.District)
                &&
                (!request.AppointmentDate.HasValue || a.AppointmentDate == request.AppointmentDate)
                &&
                (!request.IsClosed.HasValue || a.IsClosed == request.IsClosed)
            );
            dbQueryOptions.filter = filter;

            List<Appointment> result = await _repository.GetDatasAsync(dbQueryOptions);

            return _mapper.Map<List<GetAppointmentsByFilterQueryResult>>(result);
        }
    }
}
