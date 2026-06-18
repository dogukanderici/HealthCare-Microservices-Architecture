using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Persistance.MessageBus.Consumers;

namespace HealthCare.Descriptions.WebAPI.Services.BackgroundServices
{
    public class UserModifiedEventConsumerBackgroundService : BackgroundService
    {
        private readonly IUserModifiedEventConsumer _userEventConsumer;
        private readonly ILogger<UserModifiedEventConsumerBackgroundService> _logger;

        public UserModifiedEventConsumerBackgroundService(IUserModifiedEventConsumer userEventConsumer, ILogger<UserModifiedEventConsumerBackgroundService> logger)
        {
            _userEventConsumer = userEventConsumer;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("UserModifiedEventConsumerBackgroundService has started!");

            await _userEventConsumer.ConsumerAsync(stoppingToken);

            _logger.LogInformation("UserModifiedEventConsumerBackgroundService has stoppped!");
        }
    }
}
