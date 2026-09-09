using AutoMapper;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.Cities.Commands;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.Cities;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.Cities.Handlers
{
    public class UpdateCityCommandHandler : IRequestHandler<UpdateCityCommand, InternalHandlerResponse<DateTimeOffset>>
    {
        private readonly ICityCommandService _cityCommandService;
        private readonly IMapper _mapper;

        public UpdateCityCommandHandler(ICityCommandService cityCommandService, IMapper mapper)
        {
            _cityCommandService = cityCommandService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<DateTimeOffset>> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<City> existedData = await _cityCommandService.GetDataForUpdateAsync(request.Id);

            if (existedData.IsSuccess)
            {
                _mapper.Map(request, existedData.Data);

                InternalServiceResponse<DateTimeOffset> serviceResult = await _cityCommandService.UpdateAsync(existedData.Data);

                return ServiceResponseExtension.ToHandlerResponse(serviceResult);
            }

            return InternalHandlerResponse<DateTimeOffset>.Failure();

        }
    }
}