using AutoMapper;
using HealthCare.Operations.Application.Features.Mediator.Commands.AppoinmentCommands;
using HealthCare.Operations.Application.Features.Mediator.Queries.AppoinmentQueries;
using HealthCare.Operations.Application.Features.Mediator.Results.AppoinmentResults;
using HealthCare.Operations.Application.Interfaces;
using HealthCare.Operations.Configurations;
using HealthCare.Operations.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Application.Features.Mediator.Handlers.AppoinmentHandlers
{
    public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand>
    {

        private readonly IRepository<Appointment> _repository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DeleteAppointmentCommandHandler(IRepository<Appointment> repository, IMediator mediator, IMapper mapper)
        {
            _repository = repository;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            GetAppointmentByIdQueryResult result = await _mediator.Send(new GetAppointmentByIdQuery(request.Id));

            if (result != null)
            {
                Appointment dataFromDto = _mapper.Map<Appointment>(result);

                await _repository.DeleteDataAsync(dataFromDto);
            }
        }
    }
}
