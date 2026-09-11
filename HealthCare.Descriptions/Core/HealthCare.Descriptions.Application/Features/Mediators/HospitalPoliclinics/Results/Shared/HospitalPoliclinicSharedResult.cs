using HealthCare.Descriptions.Application.Features.Mediators.Hospitals.Results.Shared;
using HealthCare.Descriptions.Application.Features.Mediators.Policlinics.Results.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinics.Results.Shared
{
    public class HospitalPoliclinicSharedResult
    {
        public HospitalSharedResult Hospital { get; set; }
        public PoliclinicSharedResult Policlinic { get; set; }
    }
}