using HealthCare.Descriptions.Application.Features.Mediator.Results.DistrictResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Queries.DistrictQueries
{
    public class GetDistrictsByFilterQuery : IRequest<List<GetDistrictsByFilterQueryResult>>
    {
        public int? Plate { get; set; }
        public string? DistrictName { get; set; }
        public bool? IsAvailable { get; set; }

        private GetDistrictsByFilterQuery() { }

        public static GetDistrictsByFilterQuery Filter(int? plate, string? districtName, bool? isAvailable) => new GetDistrictsByFilterQuery()
        {
            Plate = plate,
            DistrictName = districtName,
            IsAvailable = isAvailable
        };
    }
}
