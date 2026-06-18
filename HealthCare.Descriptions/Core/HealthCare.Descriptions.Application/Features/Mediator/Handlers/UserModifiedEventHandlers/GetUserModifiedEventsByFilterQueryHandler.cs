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
    public class GetUserModifiedEventsByFilterQueryHandler : IRequestHandler<GetUserModifiedEventsByFilterQuery, List<GetUserModifiedEventsByFilterQueryResult>>
    {
        private readonly IRepository<UserModifiedEvent> _repository;
        private readonly IMapper _mapper;

        public GetUserModifiedEventsByFilterQueryHandler(IRepository<UserModifiedEvent> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetUserModifiedEventsByFilterQueryResult>> Handle(GetUserModifiedEventsByFilterQuery request, CancellationToken cancellationToken)
        {
            DbQueryOptions<UserModifiedEvent> dbQueryOptions = new DbQueryOptions<UserModifiedEvent>();

            Expression<Func<UserModifiedEvent, bool>> filter = usm => (
                (String.IsNullOrEmpty(request.Name) || usm.Name == request.Name)
                &&
                (String.IsNullOrEmpty(request.Surname) || usm.Surname == request.Surname)
                &&
                (String.IsNullOrEmpty(request.Username) || usm.Username == request.Username)
                &&
                (String.IsNullOrEmpty(request.Email) || usm.Email == request.Email)
            );
            dbQueryOptions.filter = filter;

            List<UserModifiedEvent> result = await _repository.GetDatasAsync(dbQueryOptions);

            return _mapper.Map<List<GetUserModifiedEventsByFilterQueryResult>>(result);
        }
    }
}
