using HealthCare.Descriptions.Application.Features.Mediator.Results.UserModifiedEventResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Queries.UserModifiedEventQueries
{
    public class GetUserModifiedEventsByFilterQuery : IRequest<List<GetUserModifiedEventsByFilterQueryResult>>
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }

        private GetUserModifiedEventsByFilterQuery() { }

        public static GetUserModifiedEventsByFilterQuery Filter(string? name, string? surname, string? username, string? email) =>
            new GetUserModifiedEventsByFilterQuery
            {
                Name = name,
                Surname = surname,
                Username = username,
                Email = email
            };
    }
}
