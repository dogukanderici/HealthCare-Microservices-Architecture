using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Interfaces.HandlerServices.AppointmentStatutes
{
    public interface IAppointmentStatusCommandService : ICommandBaseService<AppointmentStatus>
    {
        Task<InternalServiceResponse<AppointmentStatus>> GetDataForUpdateAsync(Guid id);
    }
}
