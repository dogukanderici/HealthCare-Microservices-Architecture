using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediator.Commands.UserModifiedEventCommands;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.UserModifiedEventHandlers
{
    public class UpdateUserModifiedEventCommandHandler : IRequestHandler<UpdateUserModifiedEventCommand>
    {
        private readonly IRepository<UserModifiedEvent> _repository;
        private readonly IMapper _mapper;

        public UpdateUserModifiedEventCommandHandler(IRepository<UserModifiedEvent> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateUserModifiedEventCommand request, CancellationToken cancellationToken)
        {
            UserModifiedEvent dataFromDto = _mapper.Map<UserModifiedEvent>(request);

            await _repository.UpdateDataAsync(dataFromDto);
        }
    }
}
