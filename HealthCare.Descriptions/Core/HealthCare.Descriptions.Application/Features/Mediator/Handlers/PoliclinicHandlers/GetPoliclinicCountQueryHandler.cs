using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.PoliclinicQueries;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.PoliclinicHandlers
{
    public class GetPoliclinicCountQueryHandler : IRequestHandler<GetPoliclinicCountQuery, int>
    {

        private readonly IRepository<Policlinic> _repository;

        public GetPoliclinicCountQueryHandler(IRepository<Policlinic> repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(GetPoliclinicCountQuery request, CancellationToken cancellationToken)
        {
            int result = await _repository.GetAllDataCountAsync();

            return result;
        }
    }
}
