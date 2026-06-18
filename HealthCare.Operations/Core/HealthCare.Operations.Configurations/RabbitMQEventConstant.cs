using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Configurations
{
    public class RabbitMQEventConstant
    {
        public static string ExchangeName = "descriptions_exchange";
        public static string QueueName = "descriptions_queue";
        public static string RouteName = "descriptions_route";
        public static string Hostname = "RabbitMQ";
        public static int Port = 5672;
        public static string Username = "guest";
        public static string Password = "guest";
    }
}
