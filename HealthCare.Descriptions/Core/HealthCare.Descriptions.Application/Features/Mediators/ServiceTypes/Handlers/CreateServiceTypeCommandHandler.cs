using AutoMapper;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.ServiceTypes.Commands;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.ServiceType;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.ServiceTypes.Handlers
{
    public class CreateServiceTypeCommandHandler : IRequestHandler<CreateServiceTypeCommand, InternalHandlerResponse<Guid>>
    {
        private readonly IServiceTypeCommandService _commandService;
        private readonly IMapper _mapper;

        public CreateServiceTypeCommandHandler(IServiceTypeCommandService commandService, IMapper mapper)
        {
            _commandService = commandService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<Guid>> Handle(CreateServiceTypeCommand request, CancellationToken cancellationToken)
        {
            ServicingType dataFromDto = _mapper.Map<ServicingType>(request);

            InternalServiceResponse<Guid> serviceResponse = await _commandService.CreateAsync(dataFromDto);

            return ServiceResponseExtension.ToHandlerResponse(serviceResponse);
        }
    }
}