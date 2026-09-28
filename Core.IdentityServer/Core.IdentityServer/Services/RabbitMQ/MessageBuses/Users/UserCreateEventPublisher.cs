using Core.IdentityServer.Commons.Constants;
using Core.IdentityServer.Commons.Helpers.RabbitMQ;
using Core.IdentityServer.Services.RabbitMQ.Events;

namespace Core.IdentityServer.Services.RabbitMQ.MessageBuses.Users
{
    public class UserCreateEventPublisher
    {
        private readonly IRabbitMQPublisherHelper<UserCreatedEvent> _publisherHelper;

        public UserCreateEventPublisher(IRabbitMQPublisherHelper<UserCreatedEvent> publisherHelper)
        {
            _publisherHelper = publisherHelper;
        }

        public async Task<bool> PublishEventAsync(UserCreatedEvent userCreatedEvent)
        {
            bool result = await _publisherHelper.PublisherAsync(userCreatedEvent, RabbitMQRoutingType.Create);

            return result;
        }
    }
}