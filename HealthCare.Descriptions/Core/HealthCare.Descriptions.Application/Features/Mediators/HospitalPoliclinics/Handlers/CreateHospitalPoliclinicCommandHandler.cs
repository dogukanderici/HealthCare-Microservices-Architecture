using AutoMapper;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinics.Commands;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.HospitalPoliclinics;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinics.Handlers
{
    public class CreateHospitalPoliclinicCommandHandler : IRequestHandler<CreateHospitalPoliclinicCommand, InternalHandlerResponse<Guid>>
    {
        private readonly IHospitalPoliclinicCommandService _commandService;
        private readonly IMapper _mapper;

        public CreateHospitalPoliclinicCommandHandler(IHospitalPoliclinicCommandService commandService, IMapper mapper)
        {
            _commandService = commandService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<Guid>> Handle(CreateHospitalPoliclinicCommand request, CancellationToken cancellationToken)
        {
            HospitalPoliclinic dataFromDto = _mapper.Map<HospitalPoliclinic>(request);

            InternalServiceResponse<Guid> serviceResponse = await _commandService.CreateAsync(dataFromDto);

            return ServiceResponseExtension.ToHandlerResponse(serviceResponse);
        }
    }
}