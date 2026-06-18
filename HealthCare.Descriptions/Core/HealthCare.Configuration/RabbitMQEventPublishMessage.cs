using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Configuration
{
    public class RabbitMQEventPublishMessage
    {
        public string EntityType { get; set; }
        public object Payload { get; set; }
    }
}
