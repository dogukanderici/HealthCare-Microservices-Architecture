using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Constants;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Features.Mediator.Commands.RabbitMQCommands;
using Core.WorkflowEngine.Application.IntegrationServices.RabbitMQ;
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
    public class UpdateUserEventConsumer : RabbitMQConsumerHelper<SyncUserEvent, UpdateSyncUserEventCommand>
    {
        protected override string QueueName => RabbitMQConstants.UpdateQueueName;
        protected override string RoutingKey => RabbitMQConstants.UpdateRoutingKey;

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