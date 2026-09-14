using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.BusinessRules.AppointmentStatuses
{
    public interface IAppointmentStatusCreatePolicy : IPolicyRule<AppointmentStatus>
    {
        // IAppointmentStatusPolicy _appointmentStatusPolicy; ile çağrılınca ExecuteAllRulesAsync() kullanmak için. Özel metotlar private olarak class'a yazılabilir.
    }
}
