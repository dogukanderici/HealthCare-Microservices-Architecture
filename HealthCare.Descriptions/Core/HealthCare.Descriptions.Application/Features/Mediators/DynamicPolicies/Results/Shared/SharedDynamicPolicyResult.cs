using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.DynamicPolicies.Results.Shared
{
    public class SharedDynamicPolicyResult
    {
        public string Methode { get; set; }
        public string Endpoint { get; set; }
        public bool IsAvailable { get; set; }
    }
}