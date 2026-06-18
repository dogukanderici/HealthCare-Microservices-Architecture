using AutoMapper;
using HealthCare.Configuration;
using HealthCare.Descriptions.Application.Features.Mediator.Commands.HospitalCommands;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.HospitalHandlers
{
    public class CreateHospitalCommandHandler : IRequestHandler<CreateHospitalCommand>
    {
        private readonly IRepository<Hospital> _repository;
        private readonly IMapper _mapper;
        private readonly IRabbitMQEventPublisher _rabbitmqEventPublisher;

        public CreateHospitalCommandHandler(IRepository<Hospital> repository, IMapper mapper, IRabbitMQEventPublisher rabbitmqEventPublisher)
        {
            _repository = repository;
            _mapper = mapper;
            _rabbitmqEventPublisher = rabbitmqEventPublisher;
        }

        public async Task Handle(CreateHospitalCommand request, CancellationToken cancellationToken)
        {
            Hospital dataFromDto = _mapper.Map<Hospital>(request);

            await _repository.CreateDataAsync(dataFromDto);

            RabbitMQEventPublishMessage publisherMessage = new RabbitMQEventPublishMessage();

            publisherMessage.Payload = request;
            publisherMessage.EntityType = RabbitMQEventConstant.CreateHospitalProjectionCommand;

            await _rabbitmqEventPublisher.PublishAsync(publisherMessage,
                RabbitMQEventConstant.ExchangeName,
                RabbitMQEventConstant.QueueName,
                RabbitMQEventConstant.RouteName
                );
        }
    }
}
