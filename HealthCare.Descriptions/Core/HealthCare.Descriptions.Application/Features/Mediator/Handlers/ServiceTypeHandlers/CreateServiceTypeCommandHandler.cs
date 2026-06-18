using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediator.Commands.ServiceCommands;
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
    public class CreateServiceTypeCommandHandler : IRequestHandler<CreateServiceTypeCommand>
    {

        private readonly IRepository<ServiceType> _repository;
        private readonly IMapper _mapper;

        public CreateServiceTypeCommandHandler(IRepository<ServiceType> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(CreateServiceTypeCommand request, CancellationToken cancellationToken)
        {
            ServiceType dataFromDto = _mapper.Map<ServiceType>(request);

            await _repository.CreateDataAsync(dataFromDto);
        }
    }
}
