using AutoMapper;
using HealthCare.Operations.Application.Features.Mediator.Commands.BillCommands;
using HealthCare.Operations.Application.Interfaces;
using HealthCare.Operations.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Application.Features.Mediator.Handlers.BillHandlers
{
    public class UpdateBillCommandHandler : IRequestHandler<UpdateBillCommand>
    {

        private readonly IRepository<Bill> _repository;
        private readonly IMapper _mapper;

        public UpdateBillCommandHandler(IRepository<Bill> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateBillCommand request, CancellationToken cancellationToken)
        {
            Bill dataFromDto = _mapper.Map<Bill>(request);

            await _repository.UpdateDataAsync(dataFromDto);
        }
    }
}
