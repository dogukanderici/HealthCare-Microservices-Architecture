namespace Core.IdentityServer.Commons.Constants
{
    public static class RabbitMQRoutingType
    {
        public static readonly string Create = "user.created";
        public static readonly string Update = "user.updated";
        public static readonly string Delete = "user.deleted";
    }
}