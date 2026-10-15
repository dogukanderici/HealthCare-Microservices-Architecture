using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Common.Constants
{
    public static class RabbitMQQueueKey
    {
        public static class Create
        {
            public static readonly string User = "user_created_description_service";
            public static readonly string Role = "role_created_description_service";
        }

        public static class Update
        {
            public static readonly string User = "user_updated_description_service";
            public static readonly string Role = "role_updated_description_service";
        }

        public static class Delete
        {
            public static readonly string User = "user_deleted_description_service";
            public static readonly string Role = "role_deleted_description_service";
        }
    }
}