using AutoMapper;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.Extensions;
using HealthCare.Descriptions.Application.Features.Mediators.Cities.Commands;
using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
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
    public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, InternalHandlerResponse<Guid>>
    {
        private readonly ICityCommandService _cityCommandService;
        private readonly IMapper _mapper;

        public CreateCityCommandHandler(ICityCommandService cityCommandService, IMapper mapper)
        {
            _cityCommandService = cityCommandService;
            _mapper = mapper;
        }

        public async Task<InternalHandlerResponse<Guid>> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            City dataFromDto = _mapper.Map<City>(request);

            InternalServiceResponse<Guid> serviceResult = await _cityCommandService.CreateAsync(dataFromDto);

            return ServiceResponseExtension.ToHandlerResponse(serviceResult);
        }
    }
}