namespace Core.IdentityServer.Services.RabbitMQ.Events.Users
{
    public class UserSoftDeletedEvent : IRabbitMQEvent
    {
        public Guid Id { get; set; }
    }
}