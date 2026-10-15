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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Persistence.IntegrationServices.RabbitMQ.RoleEvent
{
    public class RoleUpdateEventConsumer : RabbitMQConsumerHelper<SyncedRole, SyncRoleEvent, UpdateSyncedRoleCommand>
    {
        protected override string QueueName => RabbitMQQueueKey.Update.Role;
        protected override string RoutingKey => RabbitMQRoutingKey.Update.Role;

        public RoleUpdateEventConsumer(
                IOptions<RabbitMQOptions> options,
                IRabbitMQConnectionHelper connectionHelper,
                IMapper mapper,
                ILogger<RoleUpdateEventConsumer> logger,
                IServiceScopeFactory scopeFactory
            ) : base(options, connectionHelper, mapper, logger, scopeFactory)
        {

        }
    }
}