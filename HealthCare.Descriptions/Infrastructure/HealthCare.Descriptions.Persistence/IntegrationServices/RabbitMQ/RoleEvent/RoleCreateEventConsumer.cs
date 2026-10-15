using AutoMapper;
using HealthCare.Descriptions.Application.Common.Constants;
using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Features.Mediators.RabbitMQ.RoleEvent.Commands;
using HealthCare.Descriptions.Application.IntegrationServices.RabbitMQ;
using HealthCare.Descriptions.Domain.Entities;
using HealthCare.Descriptions.Persistence.Helpers.RabbitMQHelpers;
using HealthCare.Descriptions.Persistence.Helpers.RabbitMQHelpers.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HealthCare.Descriptions.Persistence.IntegrationServices.RabbitMQ.RoleEvent
{
    public class RoleCreateEventConsumer : RabbitMQConsumerHelper<SyncedRole, SyncRoleEvent, CreateSyncedRoleCommand>
    {
        protected override string QueueName => RabbitMQQueueKey.Create.Role;
        protected override string RoutingKey => RabbitMQRoutingKey.Create.Role;

        public RoleCreateEventConsumer(
                IOptions<RabbitMQOptions> options,
                IRabbitMQConnectionHelper connectionHelper,
                IMapper mapper,
                ILogger<RoleCreateEventConsumer> logger,
                IServiceScopeFactory scopeFactory
            ) : base(options, connectionHelper, mapper, logger, scopeFactory)
        {

        }
    }
}
