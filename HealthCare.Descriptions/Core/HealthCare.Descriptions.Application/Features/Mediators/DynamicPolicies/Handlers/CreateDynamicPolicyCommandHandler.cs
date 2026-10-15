using AutoMapper;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.DynamicPolicies.Commands;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.DynamicPolicies;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.DynamicPolicies.Handlers
{
    public class CreateDynamicPolicyCommandHandler : IRequestHandler<CreateDynamicPolicyCommand, InternalHandlerResponse<Guid>>
    {
        private readonly IDynamicPolicyCommandService _commandService;
        private readonly IMapper _mapper;

        public CreateDynamicPolicyCommandHandler(IDynamicPolicyCommandService commandService, IMapper mapper)
        {
            _commandService = commandService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<Guid>> Handle(CreateDynamicPolicyCommand request, CancellationToken cancellationToken)
        {
            DynamicPolicy dataFromDto = _mapper.Map<DynamicPolicy>(request);

            InternalServiceResponse<Guid> serviceResponse = await _commandService.CreateAsync(dataFromDto);

            return serviceResponse.ToHandlerResponse();
        }
    }
}