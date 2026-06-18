using AutoMapper;
using HealthCare.Configuration;
using HealthCare.Descriptions.Application.Features.Mediator.Commands.DistrictCommands;
using HealthCare.Descriptions.Application.Features.Mediator.Queries.DistrictQueries;
using HealthCare.Descriptions.Application.Features.Mediator.Results.DistrictResults;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Handlers.DistrictHandlers
{
    public class DeleteDistrictCommandHandler : IRequestHandler<DeleteDistrictCommand>
    {
        private readonly IRepository<District> _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public DeleteDistrictCommandHandler(IRepository<District> repository, IMapper mapper, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task Handle(DeleteDistrictCommand request, CancellationToken cancellationToken)
        {
            GetDistrictByIdQueryResult result = await _mediator.Send(new GetDistrictByIdQuery(request.Id));

            if (result != null)
            {
                District dataFromDto = _mapper.Map<District>(result);

                await _repository.DeleteDataAsync(dataFromDto);
            }
        }
    }
}
