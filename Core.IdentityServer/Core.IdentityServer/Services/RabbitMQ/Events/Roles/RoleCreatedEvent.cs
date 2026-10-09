namespace Core.IdentityServer.Services.RabbitMQ.Events.Roles
{
    public class RoleCreatedEvent : IRabbitMQEvent
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
    }
}