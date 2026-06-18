using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.PoliclinicQueries;
using HealthCare.Descriptions.Application.Features.Mediator.Results.PoliclinicResults;
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
    public class GetPoliclinicsQueryHandler : IRequestHandler<GetPoliclinicsQuery, List<GetPoliclinicsQueryResult>>
    {
        private readonly IRepository<Policlinic> _repository;
        private readonly IMapper _mapper;

        public GetPoliclinicsQueryHandler(IRepository<Policlinic> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetPoliclinicsQueryResult>> Handle(GetPoliclinicsQuery request, CancellationToken cancellationToken)
        {
            List<Policlinic> result = await _repository.GetDatasAsync();

            return _mapper.Map<List<GetPoliclinicsQueryResult>>(result);
        }
    }
}
