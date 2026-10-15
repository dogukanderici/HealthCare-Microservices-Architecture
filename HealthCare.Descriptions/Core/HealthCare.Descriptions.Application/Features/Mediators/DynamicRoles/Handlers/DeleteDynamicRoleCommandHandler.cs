using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.DynamicRoles.Commands;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.DynamicRoles;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.DynamicRoles.Handlers
{
    public class DeleteDynamicRoleCommandHandler : IRequestHandler<DeleteDynamicRoleCommand, InternalHandlerResponse<bool>>
    {
        private readonly IDynamicRoleCommandService _commandService;

        public DeleteDynamicRoleCommandHandler(IDynamicRoleCommandService commandService)
        {
            _commandService = commandService;
        }

        public async Task<InternalHandlerResponse<bool>> Handle(DeleteDynamicRoleCommand request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<bool> serviceResponse = await _commandService.RemoveAsync(request.Id);

            return serviceResponse.ToHandlerResponse();
        }
    }
}
