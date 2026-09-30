using AutoMapper;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.RabbitMQ.UserEvent.Commands;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.IntegrationServices.RabbitMQ;
using HealthCare.Descriptions.Application.Services.HandlerServices.RabbitMQ;
using MediatR;

namespace HealthCare.Descriptions.Application.Features.Mediators.RabbitMQ.UserEvent.Handlers
{
    public class CreateUserEventCommandHandler : IRequestHandler<CreateUserEventCommand, InternalHandlerResponse<Guid>>
    {
        private readonly ISyncUserEventCommandService _commandService;
        private readonly IMapper _mapper;

        public CreateUserEventCommandHandler(ISyncUserEventCommandService commandService, IMapper mapper)
        {
            _commandService = commandService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<Guid>> Handle(CreateUserEventCommand request, CancellationToken cancellationToken)
        {
            SyncUserEvent dataFromDto = _mapper.Map<SyncUserEvent>(request);

            InternalServiceResponse<Guid> serviceResponse = await _commandService.CreateAsync(dataFromDto);

            return serviceResponse.ToHandlerResponse();
        }
    }
}