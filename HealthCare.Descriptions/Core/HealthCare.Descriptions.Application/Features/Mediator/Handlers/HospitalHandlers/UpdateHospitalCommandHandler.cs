using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediator.Commands.HospitalCommands;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.HospitalHandlers
{
    public class UpdateHospitalCommandHandler : IRequestHandler<UpdateHospitalCommand>
    {

        private readonly IRepository<Hospital> _repository;
        private readonly IMapper _mapper;

        public UpdateHospitalCommandHandler(IRepository<Hospital> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateHospitalCommand request, CancellationToken cancellationToken)
        {
            Hospital dataFromDto = _mapper.Map<Hospital>(request);

            await _repository.UpdateDataAsync(dataFromDto);
        }
    }
}
