using AutoMapper;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.QuotaTypes.Commands;
using HealthCare.Descriptions.Application.Features.Mediators.QuotaTypes.Queries;
using HealthCare.Descriptions.Application.Features.Mediators.QuotaTypes.Results;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.QuotaTypes;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.QuotaTypes.Handlers
{
    public class CreateQuotaTypeCommandHandler : IRequestHandler<CreateQuotaTypeCommand, InternalHandlerResponse<Guid>>
    {
        private readonly IQuotaTypeCommandService _commandService;
        private readonly IMapper _mapper;

        public CreateQuotaTypeCommandHandler(IQuotaTypeCommandService commandService, IMapper mapper)
        {
            _commandService = commandService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<Guid>> Handle(CreateQuotaTypeCommand request, CancellationToken cancellationToken)
        {
            QuotaType dataFromDto = _mapper.Map<QuotaType>(request);

            InternalServiceResponse<Guid> serviceResponse = await _commandService.CreateAsync(dataFromDto);

            return ServiceResponseExtension.ToHandlerResponse(serviceResponse);
        }
    }
}