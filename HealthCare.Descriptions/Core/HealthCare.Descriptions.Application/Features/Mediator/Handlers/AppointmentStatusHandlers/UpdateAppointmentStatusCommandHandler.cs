using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediator.Commands.AppointmentStatusCommands;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.AppointmentStatusHandlers
{
    public class UpdateAppointmentStatusCommandHandler : IRequestHandler<UpdateAppointmentStatusCommand>
    {
        private readonly IRepository<AppointmentStatus> _repository;
        private readonly IMapper _mapper;

        public UpdateAppointmentStatusCommandHandler(IRepository<AppointmentStatus> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateAppointmentStatusCommand request, CancellationToken cancellationToken)
        {
            AppointmentStatus dataFromDto = _mapper.Map<AppointmentStatus>(request);

            await _repository.UpdateDataAsync(dataFromDto);
        }
    }
}
