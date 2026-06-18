using HealthCare.Descriptions.Application.Features.Mediator.Results.HospitalResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Queries.HospitalQueries
{
    public class GetHospitalsByFilterQuery : IRequest<List<GetHospitalsByFilterQueryResult>>
    {
        public int? HospitalCode { get; set; }
        public string? HospitalName { get; set; }
        public int? HospitalCity { get; set; }
        public Guid? HospitalDistrict { get; set; }
        public bool? IsAvailable { get; set; }

        private GetHospitalsByFilterQuery() { }

        public static GetHospitalsByFilterQuery Filter(int? hospitalCode, string? hospitalName, int? hospitalCity, Guid? hospitalDistrict, bool? isAvailable)
            => new GetHospitalsByFilterQuery
            {
                HospitalCode = hospitalCode,
                HospitalName = hospitalName,
                HospitalCity = hospitalCity,
                HospitalDistrict = hospitalDistrict,
                IsAvailable = isAvailable
            };
    }
}
