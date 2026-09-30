using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Common.Constants
{
    public static class RabbitMQRoutingKey
    {
        public static readonly string Create = "user.created";
        public static readonly string Update = "user.updated";
        public static readonly string Delete = "user.deleted";
    }
}