using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Features.Mediators.Cities.Results.Shared;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.Districts.Results
{
    public class GetDistrictsQueryResult : IListResult, IGenericAuditResult
    {
        public Guid Id { get; set; }
        public Guid CityId { get; set; }
        public int Plate { get; set; }
        public string DistrictName { get; set; }

        public CitySharedResult City { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public Guid UpdatedBy { get; set; }
        public bool IsAvailable { get; set; }
    }
}