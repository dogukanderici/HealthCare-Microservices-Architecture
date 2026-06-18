using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.HospitalPoliclinicQuotaQueries;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.HospitalPoliclinicQuotaHandlers
{
    public class GetHospitalPoliclinicQuotaCountQueryHandler : IRequestHandler<GetHospitalPoliclinicQuotaCountQuery, int>
    {
        private readonly IRepository<HospitalPoliclinicQuota> _repository;

        public GetHospitalPoliclinicQuotaCountQueryHandler(IRepository<HospitalPoliclinicQuota> repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(GetHospitalPoliclinicQuotaCountQuery request, CancellationToken cancellationToken)
        {
            int result = await _repository.GetAllDataCountAsync();

            return result;
        }
    }
}
