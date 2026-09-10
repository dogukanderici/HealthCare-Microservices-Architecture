using AutoMapper;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.Hospitals.Commands;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.Hospitals;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.Hospitals.Handlers
{
    public class UpdateHospitalCommandHandler : IRequestHandler<UpdateHospitalCommand, InternalHandlerResponse<DateTimeOffset>>
    {
        private readonly IHospitalCommandService _commandService;
        private readonly IMapper _mapper;

        public UpdateHospitalCommandHandler(IHospitalCommandService commandService, IMapper mapper)
        {
            _commandService = commandService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<DateTimeOffset>> Handle(UpdateHospitalCommand request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<Hospital> existedData = await _commandService.GetDataForUpdateAsync(request.Id);

            if (existedData == null)
            {
                return InternalHandlerResponse<DateTimeOffset>.Failure();
            }

            _mapper.Map(request, existedData.Data);

            InternalServiceResponse<DateTimeOffset> serviceResponse = await _commandService.UpdateAsync(existedData.Data);

            return ServiceResponseExtension.ToHandlerResponse(serviceResponse);
        }
    }
}