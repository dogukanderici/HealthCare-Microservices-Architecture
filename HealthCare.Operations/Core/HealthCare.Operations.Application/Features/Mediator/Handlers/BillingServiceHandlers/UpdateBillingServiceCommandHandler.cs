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
    public class UpdateBillingServiceCommandHandler : IRequestHandler<UpdateBillingServiceCommand>
    {

        private readonly IRepository<BillingService> _repository;
        private readonly IMapper _mapper;

        public UpdateBillingServiceCommandHandler(IRepository<BillingService> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateBillingServiceCommand request, CancellationToken cancellationToken)
        {
            BillingService dataFromDto = _mapper.Map<BillingService>(request);

            await _repository.UpdateDataAsync(dataFromDto);
        }
    }
}
