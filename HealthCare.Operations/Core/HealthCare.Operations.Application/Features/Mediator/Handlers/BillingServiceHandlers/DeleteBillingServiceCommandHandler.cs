using AutoMapper;
using HealthCare.Operations.Application.Features.Mediator.Commands.BillingServiceCommands;
using HealthCare.Operations.Application.Features.Mediator.Queries.BillingServiceQueries;
using HealthCare.Operations.Application.Features.Mediator.Results.BillingServiceResults;
using HealthCare.Operations.Application.Interfaces;
using HealthCare.Operations.Configurations;
using HealthCare.Operations.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Application.Features.Mediator.Handlers.BillingServiceHandlers
{
    public class DeleteBillingServiceCommandHandler : IRequestHandler<DeleteBillingServiceCommand>
    {

        private readonly IRepository<BillingService> _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public DeleteBillingServiceCommandHandler(IRepository<BillingService> repository, IMapper mapper, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task Handle(DeleteBillingServiceCommand request, CancellationToken cancellationToken)
        {
            GetBillingServiceByIdQueryResult result = await _mediator.Send(new GetBillingServiceByIdQuery(request.Id));

            if (result != null)
            {
                BillingService dataFromDto = _mapper.Map<BillingService>(result);

                await _repository.DeleteDataAsync(dataFromDto);
            }
        }
    }
}
