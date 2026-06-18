using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediator.Commands.CityCommands;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.CityQueries;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.ServiceQueries;
using HealthCare.Descriptions.Application.Features.Mediator.Results.CityResults;
using HealthCare.Descriptions.Application.Features.Mediator.Results.ServiceResults;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.CityHandlers
{
    public class UpdateCityCommandHandler : IRequestHandler<UpdateCityCommand>
    {

        private readonly IRepository<City> _repository;
        private readonly IMapper _mapper;

        public UpdateCityCommandHandler(IRepository<City> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateCityCommand request, CancellationToken cancellationToken)
        {
            City dataFromDto = _mapper.Map<City>(request);

            await _repository.UpdateDataAsync(dataFromDto);
        }
    }
}
