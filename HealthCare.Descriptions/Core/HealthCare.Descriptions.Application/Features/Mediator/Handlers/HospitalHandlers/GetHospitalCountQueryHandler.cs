using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.HospitalQueries;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.HospitalHandlers
{
    public class GetHospitalCountQueryHandler : IRequestHandler<GetHospitalCountQuery, int>
    {

        private readonly IRepository<Hospital> _repository;

        public GetHospitalCountQueryHandler(IRepository<Hospital> repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(GetHospitalCountQuery request, CancellationToken cancellationToken)
        {
            int result = await _repository.GetAllDataCountAsync();

            return result;
        }
    }
}
