using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediator.Commands.PoliclinicCommands;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.PoliclinicQueries;
using HealthCare.Descriptions.Application.Features.Mediator.Results.PoliclinicResults;
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
    public class DeletePoliclinicCommandHandler : IRequestHandler<DeletePoliclinicCommand>
    {

        private readonly IRepository<Policlinic> _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public DeletePoliclinicCommandHandler(IRepository<Policlinic> repository, IMapper mapper, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task Handle(DeletePoliclinicCommand request, CancellationToken cancellationToken)
        {
            GetPoliclinicByIdQueryResult result = await _mediator.Send(new GetPoliclinicByIdQuery(request.Id));

            if (result != null)
            {
                Policlinic dataFromDto = _mapper.Map<Policlinic>(result);

                await _repository.DeleteDataAsync(dataFromDto);
            }
        }
    }
}
