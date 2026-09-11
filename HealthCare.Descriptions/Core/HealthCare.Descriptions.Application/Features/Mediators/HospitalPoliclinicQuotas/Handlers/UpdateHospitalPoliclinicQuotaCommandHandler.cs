using AutoMapper;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinicQuotas.Commands;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.HospitalPoliclinicQuotas;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.HospitalPoliclinicQuotas.Handlers
{
    public class UpdateHospitalPoliclinicQuotaCommandHandler : IRequestHandler<UpdateHospitalPoliclinicQuotaCommand, InternalHandlerResponse<DateTimeOffset>>
    {
        private readonly IHospitalPoliclinicQuotaCommandService _commandService;
        private readonly IMapper _mapper;

        public UpdateHospitalPoliclinicQuotaCommandHandler(IHospitalPoliclinicQuotaCommandService commandService, IMapper mapper)
        {
            _commandService = commandService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<DateTimeOffset>> Handle(UpdateHospitalPoliclinicQuotaCommand request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<HospitalPoliclinicQuota> existedData = await _commandService.GetDataForUpdateAsync(request.Id);

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