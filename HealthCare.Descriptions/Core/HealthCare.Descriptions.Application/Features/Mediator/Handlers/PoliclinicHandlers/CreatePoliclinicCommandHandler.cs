using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediator.Commands.PoliclinicCommands;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.PoliclinicHandlers
{
    public class CreatePoliclinicCommandHandler : IRequestHandler<CreatePoliclinicCommand>
    {

        private readonly IRepository<Policlinic> _repository;
        private readonly IMapper _mapper;

        public CreatePoliclinicCommandHandler(IRepository<Policlinic> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(CreatePoliclinicCommand request, CancellationToken cancellationToken)
        {
            Policlinic dataFromDto = _mapper.Map<Policlinic>(request);

            await _repository.CreateDataAsync(dataFromDto);
        }
    }
}
