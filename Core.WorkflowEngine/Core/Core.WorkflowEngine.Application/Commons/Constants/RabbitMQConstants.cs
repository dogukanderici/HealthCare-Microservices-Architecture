using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Commons.Constants
{
    public static class RabbitMQConstants
    {
        public static readonly string CreateQueueName = "user_created_workflow_engine";
        public static readonly string UpdateQueueName = "user_updated_workflow_engine";
        public static readonly string DeleteQueueName = "user_deleted_workflow_engine";

        public static readonly string CreateRoutingKey = "user.created";
        public static readonly string UpdateRoutingKey = "user.updated";
        public static readonly string DeleteRoutingKey = "user.deleted";
    }
}
