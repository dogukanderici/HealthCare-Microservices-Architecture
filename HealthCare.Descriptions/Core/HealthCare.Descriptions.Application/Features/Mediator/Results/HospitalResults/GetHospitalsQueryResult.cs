using HealthCare.Descriptions.Application.Features.Mediator.Results.CityResults;
using HealthCare.Descriptions.Application.Features.Mediator.Results.DistrictResults;
using HealthCare.Descriptions.Application.Features.Mediator.Results.GenericResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Results.HospitalResults
{
    public class GetHospitalsQueryResult : GenericAuditResult
    {
        public Guid Id { get; set; }
        public int HospitalCode { get; set; }
        public string HospitalName { get; set; }
        public int HospitalCity { get; set; }
        public Guid HospitalDistrict { get; set; }
        public bool IsAvailable { get; set; }

        public GetCityForHospitalQueryResult City { get; set; }
        public GetDistrictForHospitalQueryResult District { get; set; }
    }
}
