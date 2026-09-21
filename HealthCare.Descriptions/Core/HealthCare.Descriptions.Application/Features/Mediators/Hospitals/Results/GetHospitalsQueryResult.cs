using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Features.Mediators.Cities.Results.Shared;
using HealthCare.Descriptions.Application.Features.Mediators.Districts.Results.Shared;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.Hospitals.Results
{
    public class GetHospitalsQueryResult : IListResult, IGenericAuditResult

    {
        public Guid Id { get; set; }
        public string HospitalCode { get; set; }
        public string HospitalName { get; set; }
        public Guid HospitalCity { get; set; }
        public Guid HospitalDistrict { get; set; }
        public bool IsAvailable { get; set; } = true;
        public DateTimeOffset CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public Guid UpdatedBy { get; set; }

        public CitySharedResult City { get; set; }
        public DistrictSharedResult District { get; set; }
    }
}