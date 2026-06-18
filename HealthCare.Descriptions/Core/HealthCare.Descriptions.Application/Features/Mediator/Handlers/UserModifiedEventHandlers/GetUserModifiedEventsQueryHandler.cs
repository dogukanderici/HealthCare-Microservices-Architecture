using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.UserModifiedEventQueries;
using HealthCare.Descriptions.Application.Features.Mediator.Results.UserModifiedEventResults;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.UserModifiedEventHandlers
{
    public class GetUserModifiedEventsQueryHandler : IRequestHandler<GetUserModifiedEventsQuery, List<GetUserModifiedEventsQueryResult>>
    {
        private readonly IRepository<UserModifiedEvent> _repository;
        private readonly IMapper _mapper;

        public GetUserModifiedEventsQueryHandler(IRepository<UserModifiedEvent> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetUserModifiedEventsQueryResult>> Handle(GetUserModifiedEventsQuery request, CancellationToken cancellationToken)
        {
            List<UserModifiedEvent> result = await _repository.GetDatasAsync();

            return _mapper.Map<List<GetUserModifiedEventsQueryResult>>(result);
        }
    }
}
