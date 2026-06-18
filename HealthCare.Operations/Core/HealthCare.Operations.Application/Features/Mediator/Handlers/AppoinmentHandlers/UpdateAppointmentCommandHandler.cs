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
    public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand>
    {

        private readonly IRepository<Appointment> _repository;
        private readonly IMapper _mapper;

        public UpdateAppointmentCommandHandler(IRepository<Appointment> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
        {
            Appointment dataFromDto = _mapper.Map<Appointment>(request);

            await _repository.UpdateDataAsync(dataFromDto);
        }
    }
}
