using AutoMapper;
using HealthCare.Configuration;
using HealthCare.Descriptions.Application.Features.Mediator.Commands.HospitalCommands;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.HospitalQueries;
using HealthCare.Descriptions.Application.Features.Mediator.Results.HospitalResults;
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
    public class DeleteHospitalCommandHandler : IRequestHandler<DeleteHospitalCommand>
    {

        private readonly IRepository<Hospital> _repository;
        private readonly IMediator _madiator;
        private readonly IMapper _mapper;

        public DeleteHospitalCommandHandler(IRepository<Hospital> repository, IMediator madiator, IMapper mapper)
        {
            _repository = repository;
            _madiator = madiator;
            _mapper = mapper;
        }

        public async Task Handle(DeleteHospitalCommand request, CancellationToken cancellationToken)
        {
            GetHospitalByIdQueryResult result = await _madiator.Send(new GetHospitalByIdQuery(request.Id));

            if (result != null)
            {
                await _repository.DeleteDataAsync(_mapper.Map<Hospital>(result));
            }
        }
    }
}
