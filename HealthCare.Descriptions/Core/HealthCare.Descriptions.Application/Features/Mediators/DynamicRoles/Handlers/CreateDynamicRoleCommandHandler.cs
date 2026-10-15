using AutoMapper;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.DynamicRoles.Commands;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.DynamicRoles;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.DynamicRoles.Handlers
{
    public class CreateDynamicRoleCommandHandler : IRequestHandler<CreateDynamicRoleCommand, InternalHandlerResponse<Guid>>
    {
        private readonly IDynamicRoleCommandService _commandService;
        private readonly IMapper _mapper;

        public CreateDynamicRoleCommandHandler(IDynamicRoleCommandService commandService, IMapper mapper)
        {
            _commandService = commandService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<Guid>> Handle(CreateDynamicRoleCommand request, CancellationToken cancellationToken)
        {
            DynamicRole dataFromDto = _mapper.Map<DynamicRole>(request);

            InternalServiceResponse<Guid> serviceResponse = await _commandService.CreateAsync(dataFromDto);

            return serviceResponse.ToHandlerResponse();
        }
    }
}