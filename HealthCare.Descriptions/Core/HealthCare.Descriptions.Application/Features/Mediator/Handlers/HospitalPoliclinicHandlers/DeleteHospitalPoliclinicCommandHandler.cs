using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediator.Commands.HospitalPoliclinicCommands;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.HospitalPoliclinicQueries;
using HealthCare.Descriptions.Application.Features.Mediator.Results.HospitalPoliclinicResults;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.HospitalPoliclinicHandlers
{
    public class DeleteHospitalPoliclinicCommandHandler : IRequestHandler<DeleteHospitalPoliclinicCommand>
    {

        private readonly IRepository<HospitalPoliclinic> _repository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DeleteHospitalPoliclinicCommandHandler(IRepository<HospitalPoliclinic> repository, IMediator mediator, IMapper mapper)
        {
            _repository = repository;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task Handle(DeleteHospitalPoliclinicCommand request, CancellationToken cancellationToken)
        {
            GetHospitalPoliclinicByIdQueryResult result = await _mediator.Send(new GetHospitalPoliclinicByIdQuery(request.Id));

            if (result != null)
            {
                await _repository.DeleteDataAsync(_mapper.Map<HospitalPoliclinic>(result));
            }
        }
    }
}
