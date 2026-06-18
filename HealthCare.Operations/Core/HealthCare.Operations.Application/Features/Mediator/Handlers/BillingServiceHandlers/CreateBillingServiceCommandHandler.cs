using AutoMapper;
using HealthCare.Operations.Application.Features.Mediator.Commands.BillingServiceCommands;
using HealthCare.Operations.Application.Interfaces;
using HealthCare.Operations.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Application.Features.Mediator.Handlers.BillingServiceHandlers
{
    public class CreateBillingServiceCommandHandler : IRequestHandler<CreateBillingServiceCommand>
    {

        private readonly IRepository<BillingService> _repository;
        private readonly IMapper _mapper;

        public CreateBillingServiceCommandHandler(IRepository<BillingService> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(CreateBillingServiceCommand request, CancellationToken cancellationToken)
        {
            BillingService dataFromDto = _mapper.Map<BillingService>(request);

            await _repository.CreateDataAsync(dataFromDto);
        }
    }
}
