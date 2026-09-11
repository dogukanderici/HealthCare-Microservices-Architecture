using HealthCare.Descriptions.Application.Interfaces.HandlerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinics.Results
{
    public class GetHospitalPoliclinicsByFilterQueryResult : IListResult
    {
        public Guid Id { get; set; }
        public Guid HospitalId { get; set; }
        public Guid PoliclinicId { get; set; }
        public bool IsAvailable { get; set; }
    }
}
