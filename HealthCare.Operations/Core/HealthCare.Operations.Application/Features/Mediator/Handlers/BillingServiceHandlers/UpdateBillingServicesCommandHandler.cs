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
    public class UpdateBillingServicesCommandHandler : IRequestHandler<UpdateBillingServicesCommand>
    {

        private readonly IRepository<BillingService> _repository;
        private readonly IMapper _mapper;

        public UpdateBillingServicesCommandHandler(IRepository<BillingService> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateBillingServicesCommand request, CancellationToken cancellationToken)
        {
            List<BillingService> dataListFromDto = _mapper.Map<List<BillingService>>(request.UpdateListData);

            await _repository.UpdateListDataAsync(dataListFromDto);
        }
    }
}
