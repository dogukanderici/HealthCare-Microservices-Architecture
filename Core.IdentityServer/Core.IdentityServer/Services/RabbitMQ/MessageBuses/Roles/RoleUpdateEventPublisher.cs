using Core.IdentityServer.Commons.Constants;
using Core.IdentityServer.Commons.Helpers.RabbitMQ;
using Core.IdentityServer.Services.RabbitMQ.Events.Roles;

namespace Core.IdentityServer.Services.RabbitMQ.MessageBuses.Roles
{
    public class RoleUpdateEventPublisher
    {
        private readonly IRabbitMQPublisherHelper<RoleUpdatedEvent> _publisherHelper;

        public RoleUpdateEventPublisher(IRabbitMQPublisherHelper<RoleUpdatedEvent> publisherHelper)
        {
            _publisherHelper = publisherHelper;
        }

        public async Task<bool> PublishEventAsync(RoleUpdatedEvent roleUpdatedEvent)
        {
            bool publishResponse = await _publisherHelper.PublisherAsync(roleUpdatedEvent, RabbitMQRoutingType.Update.Role);

            return publishResponse;
        }
    }
}