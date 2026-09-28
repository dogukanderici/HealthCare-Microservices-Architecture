namespace Core.IdentityServer.Services.RabbitMQ.Events
{
    public class UserUpdatedEvent : IRabbitMQEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}