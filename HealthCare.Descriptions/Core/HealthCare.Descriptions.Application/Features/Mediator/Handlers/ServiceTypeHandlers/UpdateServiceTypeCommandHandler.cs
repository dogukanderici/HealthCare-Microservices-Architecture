using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediator.Commands.ServiceCommands;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.ServiceQueries;
using HealthCare.Descriptions.Application.Features.Mediator.Results.ServiceResults;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.ServiceHandlers
{
    public class UpdateServiceTypeCommandHandler : IRequestHandler<UpdateServiceTypeCommand>
    {

        private readonly IRepository<ServiceType> _repository;
        private readonly IMapper _mapper;

        public UpdateServiceTypeCommandHandler(IRepository<ServiceType> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateServiceTypeCommand request, CancellationToken cancellationToken)
        {
            ServiceType dataFromDto = _mapper.Map<ServiceType>(request);

            await _repository.UpdateDataAsync(dataFromDto);
        }
    }
}
