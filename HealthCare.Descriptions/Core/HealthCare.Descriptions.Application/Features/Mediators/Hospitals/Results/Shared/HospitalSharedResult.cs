using HealthCare.Descriptions.Application.Features.Mediators.Cities.Results.Shared;
using HealthCare.Descriptions.Application.Features.Mediators.Districts.Results.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.Hospitals.Results.Shared
{
    public class HospitalSharedResult
    {
        public string HospitalCode { get; set; }
        public string HospitalName { get; set; }

        public CitySharedResult City { get; set; }
        public DistrictSharedResult District { get; set; }
    }
}