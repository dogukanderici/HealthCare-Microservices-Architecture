using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.DistrictQueries;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.DistrictHandlers
{
    public class GetDistrictCountQueryHandler : IRequestHandler<GetDistrictCountQuery, int>
    {

        private readonly IRepository<District> _repository;

        public GetDistrictCountQueryHandler(IRepository<District> repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(GetDistrictCountQuery request, CancellationToken cancellationToken)
        {
            int result = await _repository.GetAllDataCountAsync();

            return result;
        }
    }
}
