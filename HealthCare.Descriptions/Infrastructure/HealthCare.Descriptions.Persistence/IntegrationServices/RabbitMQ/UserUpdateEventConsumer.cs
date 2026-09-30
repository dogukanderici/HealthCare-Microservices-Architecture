using AutoMapper;
using HealthCare.Descriptions.Application.Common.Constants;
using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Features.Mediators.RabbitMQ.UserEvent.Commands;
using HealthCare.Descriptions.Application.IntegrationServices.RabbitMQ;
using HealthCare.Descriptions.Persistence.Helpers.RabbitMQHelpers;
using HealthCare.Descriptions.Persistence.Helpers.RabbitMQHelpers.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Persistence.IntegrationServices.RabbitMQ
{
    public class UserUpdateEventConsumer : RabbitMQConsumerHelper<SyncUserEvent, UpdateUserEventCommand>
    {
        protected override string QueueName => "user_updated_description_service";
        protected override string RoutingKey => RabbitMQRoutingKey.Update;

        public UserUpdateEventConsumer(
                IOptions<RabbitMQOptions> options,
                IRabbitMQConnectionHelper connectionHelper,
                IMapper mapper,
                ILogger<UserUpdateEventConsumer> logger,
                IServiceScopeFactory scopeFactory
            ) : base(options, connectionHelper, mapper, logger, scopeFactory)
        {

        }
    }
}