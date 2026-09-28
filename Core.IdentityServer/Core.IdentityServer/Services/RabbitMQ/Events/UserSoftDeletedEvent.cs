namespace Core.IdentityServer.Services.RabbitMQ.Events
{
    public class UserSoftDeletedEvent : IRabbitMQEvent
    {
        public Guid Id { get; set; }
    }
}