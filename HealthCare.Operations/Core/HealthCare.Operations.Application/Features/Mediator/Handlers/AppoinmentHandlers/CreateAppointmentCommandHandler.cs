using AutoMapper;
using HealthCare.Operations.Application.Features.Mediator.Commands.AppoinmentCommands;
using HealthCare.Operations.Application.Interfaces;
using HealthCare.Operations.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Application.Features.Mediator.Handlers.AppoinmentHandlers
{
    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand>
    {

        private readonly IRepository<Appointment> _repository;
        private readonly IMapper _mapper;

        public CreateAppointmentCommandHandler(IRepository<Appointment> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            Appointment dataFromDto = _mapper.Map<Appointment>(request);

            await _repository.CreateDataAsync(dataFromDto);
        }
    }
}
