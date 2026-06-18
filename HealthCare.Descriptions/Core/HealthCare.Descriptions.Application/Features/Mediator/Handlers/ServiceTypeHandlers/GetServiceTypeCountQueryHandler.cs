using HealthCare.Descriptions.Application.Features.Mediator.Queries.ServiceQueries;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.ServiceHandlers
{
    public class GetServiceTypeCountQueryHandler : IRequestHandler<GetServiceTypeCountQuery, int>
    {
        private readonly IRepository<ServiceType> _repository;

        public GetServiceTypeCountQueryHandler(IRepository<ServiceType> repository)
        {
            _repository = repository;
        }

        async Task<int> IRequestHandler<GetServiceTypeCountQuery, int>.Handle(GetServiceTypeCountQuery request, CancellationToken cancellationToken)
        {
            int result = await _repository.GetAllDataCountAsync();

            return result;
        }
    }
}
