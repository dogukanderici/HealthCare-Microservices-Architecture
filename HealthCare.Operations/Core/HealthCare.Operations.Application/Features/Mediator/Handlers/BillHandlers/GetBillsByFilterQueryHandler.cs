using AutoMapper;
using HealthCare.Operations.Application.Features.Mediator.Queries.BillQueries;
using HealthCare.Operations.Application.Features.Mediator.Results.BillResults;
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

namespace HealthCare.Operations.Application.Features.Mediator.Handlers.BillHandlers
{
    public class GetBillsByFilterQueryHandler : IRequestHandler<GetBillsByFilterQuery, List<GetBillsByFilterQueryResult>>
    {
        private readonly IRepository<Bill> _repository;
        private readonly IMapper _mapper;

        public GetBillsByFilterQueryHandler(IRepository<Bill> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetBillsByFilterQueryResult>> Handle(GetBillsByFilterQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<Bill> queryOptions = new DbQueryOptions<Bill>();

            Expression<Func<Bill, bool>> filter = b => (
                (!request.AppointmentId.HasValue || b.AppointmentId == request.AppointmentId)
                &&
                (String.IsNullOrEmpty(request.BillingNumber) || b.BillingNumber == request.BillingNumber)
                &&
                (!request.BillingDate.HasValue || b.BillingDate == request.BillingDate)
                &&
                (String.IsNullOrEmpty(request.PatientNameSurname) || b.PatientNameSurname == request.PatientNameSurname)
                &&
                (String.IsNullOrEmpty(request.PatientPhone) || b.PatientPhone == request.PatientPhone)
                &&
                (request.PatientNationality.HasValue || b.PatientNationality == request.PatientNationality)
                &&
                (String.IsNullOrEmpty(request.PatientIDNumber) || b.PatientIDNumber == request.PatientIDNumber)
                &&
                (String.IsNullOrEmpty(request.PatientMail) || b.PatientMail == request.PatientMail)
            );

            queryOptions.filter = filter;

            List<Bill> result = await _repository.GetDatasAsync(queryOptions);

            return _mapper.Map<List<GetBillsByFilterQueryResult>>(result);
        }
    }
}
