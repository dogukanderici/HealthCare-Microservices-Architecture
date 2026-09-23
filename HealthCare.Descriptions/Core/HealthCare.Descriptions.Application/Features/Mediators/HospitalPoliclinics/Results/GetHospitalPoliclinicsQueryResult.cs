using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Features.Mediators.Hospitals.Results.Shared;
using HealthCare.Descriptions.Application.Features.Mediators.Policlinics.Results.Shared;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinics.Results
{
    public class GetHospitalPoliclinicsQueryResult : IListResult, IGenericAuditResult
    {
        public Guid Id { get; set; }
        public Guid HospitalId { get; set; }
        public Guid PoliclinicId { get; set; }
        public bool IsAvailable { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public Guid UpdatedBy { get; set; }

        public HospitalSharedResult Hospital { get; set; }
        public PoliclinicSharedResult Policlinic { get; set; }
    }
}