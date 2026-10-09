using Core.IdentityServer.Commons.Constants;
using Core.IdentityServer.Commons.Helpers.RabbitMQ;
using Core.IdentityServer.Models;
using Core.IdentityServer.Services.RabbitMQ.Events.Users;

namespace Core.IdentityServer.Services.RabbitMQ.MessageBuses.Users
{
    public class UserUpdateEventPublisher
    {
        private readonly IRabbitMQPublisherHelper<UserUpdatedEvent> _helper;

        public UserUpdateEventPublisher(IRabbitMQPublisherHelper<UserUpdatedEvent> helper)
        {
            _helper = helper;
        }

        public async Task<bool> PublishEventAsync(UserUpdatedEvent userUpdatedEvent)
        {
            return await _helper.PublisherAsync(userUpdatedEvent, RabbitMQRoutingType.Update.User);
        }
    }
}