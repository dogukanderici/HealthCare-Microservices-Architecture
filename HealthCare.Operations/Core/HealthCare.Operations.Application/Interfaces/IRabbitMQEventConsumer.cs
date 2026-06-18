using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Application.Interfaces
{
    public interface IRabbitMQEventConsumer
    {
        public Task ConsumerAsync(CancellationToken cancellationToken);
    }
}
