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
    public class UpdateHospitalPoliclinicCommandHandler : IRequestHandler<UpdateHospitalPoliclinicCommand, InternalHandlerResponse<DateTimeOffset>>
    {
        private readonly IHospitalPoliclinicCommandService _commandService;
        private readonly IMapper _mapper;

        public UpdateHospitalPoliclinicCommandHandler(IHospitalPoliclinicCommandService commandService, IMapper mapper)
        {
            _commandService = commandService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<DateTimeOffset>> Handle(UpdateHospitalPoliclinicCommand request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<HospitalPoliclinic> existedData = await _commandService.GetDataForUpdateAsync(request.Id);

            if (!existedData.IsSuccess)
            {
                return InternalHandlerResponse<DateTimeOffset>.Failure();
            }

            _mapper.Map(request, existedData.Data);

            InternalServiceResponse<DateTimeOffset> serviceResponse = await _commandService.UpdateAsync(existedData.Data);

            return ServiceResponseExtension.ToHandlerResponse(serviceResponse);
        }
    }
}