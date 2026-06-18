using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.HospitalPoliclinicQueries;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.HospitalPoliclinicHandlers
{
    public class GetHospitalPoliclinicCountQueryHandler : IRequestHandler<GetHospitalPoliclinicCountQuery, int>
    {

        private readonly IRepository<HospitalPoliclinic> _repository;

        public GetHospitalPoliclinicCountQueryHandler(IRepository<HospitalPoliclinic> repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(GetHospitalPoliclinicCountQuery request, CancellationToken cancellationToken)
        {
            int result = await _repository.GetAllDataCountAsync();

            return result;
        }
    }
}
