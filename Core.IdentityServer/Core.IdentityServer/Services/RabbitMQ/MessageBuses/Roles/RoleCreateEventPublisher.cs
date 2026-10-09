using Core.IdentityServer.Commons.Constants;
using Core.IdentityServer.Commons.Helpers.RabbitMQ;
using Core.IdentityServer.Services.RabbitMQ.Events.Roles;

namespace Core.IdentityServer.Services.RabbitMQ.MessageBuses.Roles
{
    public class RoleCreateEventPublisher
    {
        private readonly IRabbitMQPublisherHelper<RoleCreatedEvent> _publisherHelper;

        public RoleCreateEventPublisher(IRabbitMQPublisherHelper<RoleCreatedEvent> publisherHelper)
        {
            _publisherHelper = publisherHelper;
        }

        public async Task<bool> PublishEventAsync(RoleCreatedEvent roleCreatedEvent)
        {
            bool publishResponse = await _publisherHelper.PublisherAsync(roleCreatedEvent, RabbitMQRoutingType.Create.Role);

            return publishResponse;
        }
    }
}