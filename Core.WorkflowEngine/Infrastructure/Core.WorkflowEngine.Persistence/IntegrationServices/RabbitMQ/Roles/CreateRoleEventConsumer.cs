using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Constants;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.RabbitMQCommands.SyncedRoleCommands;
using Core.WorkflowEngine.Application.IntegrationServices.RabbitMQ.RoleEvent;
using Core.WorkflowEngine.Domain.Abstractions;
using Core.WorkflowEngine.Domain.Entities;
using Core.WorkflowEngine.Persistence.Commons.Helpers;
using Core.WorkflowEngine.Persistence.Commons.Helpers.Abstracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Persistence.IntegrationServices.RabbitMQ.Roles
{
    public class CreateRoleEventConsumer : RabbitMQConsumerHelper<SyncRoleEvent, SyncedRole, CreateSyncedRoleCommand>
    {
        protected override string QueueName => RabbitMQConstants.Create.RoleQueueName;
        protected override string RoutingKey => RabbitMQConstants.Create.RoleRoutingKey;

        public CreateRoleEventConsumer(
                IOptions<RabbitMQOptions> options,
                IRabbitMQConnectionHelper connectionHelper,
                IServiceScopeFactory scopeFactory,
                IMapper mapper,
                ILogger<CreateRoleEventConsumer> logger
            ) : base(options, connectionHelper, scopeFactory, mapper, logger)
        {

        }
    }
}