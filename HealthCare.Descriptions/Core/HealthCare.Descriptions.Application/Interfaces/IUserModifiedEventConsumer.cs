using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Interfaces
{
    public interface IUserModifiedEventConsumer
    {
        public Task ConsumerAsync(CancellationToken cancellationToken);
    }
}
