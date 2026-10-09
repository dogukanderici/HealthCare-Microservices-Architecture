using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Constants;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.RabbitMQCommands.SyncedUserCommands;
using Core.WorkflowEngine.Application.IntegrationServices.RabbitMQ.UserEvent;
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

namespace Core.WorkflowEngine.Persistence.IntegrationServices.RabbitMQ.Users
{
    public class UpdateUserEventConsumer : RabbitMQConsumerHelper<SyncUserEvent,SyncedUser, UpdateSyncUserEventCommand>
    {
        protected override string QueueName => RabbitMQConstants.Update.UserQueueName;
        protected override string RoutingKey => RabbitMQConstants.Update.UserRoutingKey;

        public UpdateUserEventConsumer(
                IOptions<RabbitMQOptions> options,
                IRabbitMQConnectionHelper connectionHelper,
                IServiceScopeFactory scopeFactory,
                IMapper mapper,
                ILogger<UpdateUserEventConsumer> logger
            ) : base(options, connectionHelper, scopeFactory, mapper, logger)
        {

        }
    }
}