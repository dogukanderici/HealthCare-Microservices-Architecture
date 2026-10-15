using Core.IdentityServer.Commons.Constants;
using Core.IdentityServer.Commons.Helpers.RabbitMQ;
using Core.IdentityServer.Services.RabbitMQ.Events.Roles;

namespace Core.IdentityServer.Services.RabbitMQ.MessageBuses.Roles
{
    public class RoleSoftDeleteEventPublisher
    {
        private readonly IRabbitMQPublisherHelper<RoleSoftDeletedEvent> _publisherHelper;

        public RoleSoftDeleteEventPublisher(IRabbitMQPublisherHelper<RoleSoftDeletedEvent> publisherHelper)
        {
            _publisherHelper = publisherHelper;
        }

        public async Task<bool> PublishEventAsync(RoleSoftDeletedEvent roleSoftDeletedEvent)
        {
            bool publisherResponse = await _publisherHelper.PublisherAsync(roleSoftDeletedEvent, RabbitMQRoutingType.Delete.Role);

            return publisherResponse;
        }
    }
}