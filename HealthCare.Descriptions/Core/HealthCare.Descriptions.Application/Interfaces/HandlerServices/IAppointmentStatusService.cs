using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Interfaces.HandlerServices
{
    public interface IAppointmentStatusService<T, TResult> : IBaseService<T, TResult>
        where T : class
        where TResult : class
    {

    }
}