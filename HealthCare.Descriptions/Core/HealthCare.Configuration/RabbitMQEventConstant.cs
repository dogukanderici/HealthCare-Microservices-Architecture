using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Configuration
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

        public static string CreateHospitalProjectionCommand = "CreateHospitalProjectionCommand";
        public static string UpdateHospitalProjectionCommand = "UpdateHospitalProjectionCommand";
        public static string CreatePoliclinicProjectionCommand = "CreatePoliclinicProjectionCommand";
        public static string UpdatePoliclinicProjectionCommand = "UpdatePoliclinicProjectionCommand";
    }
}
