namespace Core.IdentityServer.Commons.Constants
{
    public static class RabbitMQRoutingType
    {
        public static class Create
        {
            public static readonly string User = "user.created";
            public static readonly string Role = "role.created";
        }
        public static class Update
        {
            public static readonly string User = "user.updated";
            public static readonly string Role = "role.updated";

        }
        public static class Delete
        {
            public static readonly string User = "user.deleted";
            public static readonly string Role = "role.deleted";
        }
    }
}