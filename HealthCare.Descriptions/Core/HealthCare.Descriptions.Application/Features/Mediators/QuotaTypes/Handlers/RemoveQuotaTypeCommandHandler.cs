using AutoMapper;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.QuotaTypes.Commands;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.QuotaTypes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.QuotaTypes.Handlers
{
    public class RemoveQuotaTypeCommandHandler : IRequestHandler<RemoveQuotaTypeCommand, InternalHandlerResponse<bool>>
    {
        private readonly IQuotaTypeCommandService _commandService;

        public RemoveQuotaTypeCommandHandler(IQuotaTypeCommandService commandService)
        {
            _commandService = commandService;
        }

        public async Task<InternalHandlerResponse<bool>> Handle(RemoveQuotaTypeCommand request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<bool> serviceResponse = await _commandService.RemoveAsync(request.Id);

            return ServiceResponseExtension.ToHandlerResponse(serviceResponse);
        }
    }
}