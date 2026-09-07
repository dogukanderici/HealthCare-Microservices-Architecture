using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.AppointmentStatuses.Results
{
    public class GetAppointmentStatusesResult : GenericAuditResult, IListResult
    {
        public Guid Id { get; set; }
        public string StatusName { get; set; }
    }
}
