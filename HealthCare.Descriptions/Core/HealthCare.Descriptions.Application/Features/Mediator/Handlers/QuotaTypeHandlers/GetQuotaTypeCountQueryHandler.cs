using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.QuotaTypeQueries;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.QuotaTypeHandlers
{
    public class GetQuotaTypeCountQueryHandler : IRequestHandler<GetQuotaTypeCountQuery, int>
    {
        private readonly IRepository<QuotaType> _repository;

        public GetQuotaTypeCountQueryHandler(IRepository<QuotaType> repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(GetQuotaTypeCountQuery request, CancellationToken cancellationToken)
        {
            int result = await _repository.GetAllDataCountAsync();

            return result;
        }
    }
}
