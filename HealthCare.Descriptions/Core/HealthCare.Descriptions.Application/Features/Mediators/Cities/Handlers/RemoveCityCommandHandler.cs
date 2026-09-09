using AutoMapper;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.Cities.Commands;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.Cities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.Cities.Handlers
{
    public class RemoveCityCommandHandler : IRequestHandler<RemoveCityCommand, InternalHandlerResponse<bool>>
    {
        private readonly ICityCommandService _cityCommandService;
        private readonly IMapper _mapper;

        public RemoveCityCommandHandler(ICityCommandService cityCommandService, IMapper mapper)
        {
            _cityCommandService = cityCommandService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<bool>> Handle(RemoveCityCommand request, CancellationToken cancellationToken)
        {
            InternalServiceResponse<bool> serviceResult = await _cityCommandService.RemoveAsync(request.Id);

            return ServiceResponseExtension.ToHandlerResponse(serviceResult);
        }
    }
}