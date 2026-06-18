using HealthCare.Descriptions.Application.Features.Mediator.Results.CityResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Results.HospitalResults
{
    public class GetHospitalForHospitalPoliclinicQueryResult
    {
        public int HospitalCity { get; set; }
        public string HospitalName { get; set; }
        public GetCityForHospitalQueryResult City { get; set; }
    }
}
