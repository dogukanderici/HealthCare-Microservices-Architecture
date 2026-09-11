using AutoMapper;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinicQuotas.Commands;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.HospitalPoliclinicQuotas;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinicQuotas.Handlers
{
    public class CreateHospitalPoliclinicQuotaCommandHandler : IRequestHandler<CreateHospitalPoliclinicQuotaCommand, InternalHandlerResponse<Guid>>
    {
        private readonly IHospitalPoliclinicQuotaCommandService _commandService;
        private readonly IMapper _mapper;

        public CreateHospitalPoliclinicQuotaCommandHandler(IHospitalPoliclinicQuotaCommandService commandService, IMapper mapper)
        {
            _commandService = commandService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<Guid>> Handle(CreateHospitalPoliclinicQuotaCommand request, CancellationToken cancellationToken)
        {
            HospitalPoliclinicQuota dataFromDto = _mapper.Map<HospitalPoliclinicQuota>(request);

            InternalServiceResponse<Guid> serviceResponse = await _commandService.CreateAsync(dataFromDto);

            return ServiceResponseExtension.ToHandlerResponse(serviceResponse);
        }
    }
}