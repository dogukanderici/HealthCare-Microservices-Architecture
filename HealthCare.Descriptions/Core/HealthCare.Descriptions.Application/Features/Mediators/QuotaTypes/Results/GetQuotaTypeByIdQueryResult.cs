using HealthCare.Descriptions.Application.Interfaces.HandlerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.QuotaTypes.Results
{
    public class GetQuotaTypeByIdQueryResult : ISingleResult
    {
        public Guid Id { get; set; }
        public string QuotaTypeName { get; set; }
        public string QuotaTypeCode { get; set; }
        public bool IsAvailable { get; set; }
    }
}
