using AutoMapper;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.RabbitMQ.UserEvent.Commands;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.IntegrationServices.RabbitMQ;
using HealthCare.Descriptions.Application.Services.HandlerServices.RabbitMQ;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.RabbitMQ.UserEvent.Handlers
{
    public class UpdateUserEventCommandHandler : IRequestHandler<UpdateUserEventCommand, InternalHandlerResponse<DateTimeOffset>>
    {
        private readonly ISyncUserEventCommandService _commandService;
        private readonly IMapper _mapper;

        public UpdateUserEventCommandHandler(ISyncUserEventCommandService commandService, IMapper mapper)
        {
            _commandService = commandService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<DateTimeOffset>> Handle(UpdateUserEventCommand request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<SyncUserEvent> existedData = await _commandService.GetDataForUpdateAsync(request.Id);

            if (existedData.Data == null)
            {
                return InternalHandlerResponse<DateTimeOffset>.Failure();
            }

            _mapper.Map(request, existedData.Data);

            InternalServiceResponse<DateTimeOffset> serviceResponse = await _commandService.UpdateAsync(existedData.Data);

            return serviceResponse.ToHandlerResponse();
        }
    }
}