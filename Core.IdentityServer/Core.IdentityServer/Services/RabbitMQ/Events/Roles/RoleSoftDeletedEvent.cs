namespace Core.IdentityServer.Services.RabbitMQ.Events.Roles
{
    public class RoleSoftDeletedEvent : IRabbitMQEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = false;
    }
}