namespace Core.WorkflowEngine.Application.Commons.Constants
{
    public static class RabbitMQConstants
    {
        public static class Create
        {
            public static readonly string UserQueueName = "user_created_workflow_engine";
            public static readonly string UserRoutingKey = "user.created";

            public static readonly string RoleQueueName = "role_created_workflow_engine";
            public static readonly string RoleRoutingKey = "role.created";
        }

        public static class Update
        {
            public static readonly string UserQueueName = "user_updated_workflow_engine";
            public static readonly string UserRoutingKey = "user.updated";

            public static readonly string RoleQueueName = "role_updated_workflow_engine";
            public static readonly string RoleRoutingKey = "role.updated";

        }

        public static class Delete
        {
            public static readonly string UserQueueName = "user_deleted_workflow_engine";
            public static readonly string UserRoutingKey = "user.deleted";

            public static readonly string RoleQueueName = "role_deleted_workflow_engine";
            public static readonly string RoleRoutingKey = "role.deleted";

        }
    }
}