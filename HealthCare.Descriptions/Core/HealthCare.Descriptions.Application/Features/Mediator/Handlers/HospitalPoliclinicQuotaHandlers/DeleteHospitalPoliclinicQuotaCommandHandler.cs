using AutoMapper;
using HealthCare.Descriptions.Application.Features.Mediator.Commands.HospitalPoliclinicQuotaCommands;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.HospitalPoliclinicQuotaQueries;
using HealthCare.Descriptions.Application.Features.Mediator.Results.HospitalPoliclinicQuotaResults;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.HospitalPoliclinicQuotaHandlers
{
    public class DeleteHospitalPoliclinicQuotaCommandHandler : IRequestHandler<DeleteHospitalPoliclinicQuotaCommand>
    {

        private readonly IRepository<HospitalPoliclinicQuota> _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public DeleteHospitalPoliclinicQuotaCommandHandler(IRepository<HospitalPoliclinicQuota> repository, IMapper mapper, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task Handle(DeleteHospitalPoliclinicQuotaCommand request, CancellationToken cancellationToken)
        {
            GetHospitalPoliclinicQuotaByIdQueryResult result = await _mediator.Send(new GetHospitalPoliclinicQuotaByIdQuery(request.Id));

            if (result != null)
            {
                HospitalPoliclinicQuota dataFromDto = _mapper.Map<HospitalPoliclinicQuota>(result);

                await _repository.DeleteDataAsync(dataFromDto);
            }
        }
    }
}
