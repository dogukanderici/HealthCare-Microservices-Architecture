using AutoMapper;
using HealthCare.Configuration;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.UserModifiedEventQueries;
using HealthCare.Descriptions.Application.Features.Mediator.Results.UserModifiedEventResults;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.UserModifiedEventHandlers
{
    public class GetUserModifiedEventByIdQueryHandler : IRequestHandler<GetUserModifiedEventByIdQuery, GetUserModifiedEventByIdQueryResult>
    {
        private readonly IRepository<UserModifiedEvent> _repository;
        private readonly IMapper _mapper;

        public GetUserModifiedEventByIdQueryHandler(IRepository<UserModifiedEvent> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetUserModifiedEventByIdQueryResult> Handle(GetUserModifiedEventByIdQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<UserModifiedEvent> dbQueryOptions = new DbQueryOptions<UserModifiedEvent>();

            Expression<Func<UserModifiedEvent, bool>> filter = usm => usm.Id == request.Id;
            dbQueryOptions.filter = filter;

            UserModifiedEvent result = await _repository.GetDataAsync(dbQueryOptions);

            return _mapper.Map<GetUserModifiedEventByIdQueryResult>(result);
        }
    }
}
