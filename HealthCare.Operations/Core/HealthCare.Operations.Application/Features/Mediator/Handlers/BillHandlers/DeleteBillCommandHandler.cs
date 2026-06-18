using AutoMapper;
using HealthCare.Operations.Application.Features.Mediator.Commands.BillCommands;
using HealthCare.Operations.Application.Features.Mediator.Queries.BillQueries;
using HealthCare.Operations.Application.Features.Mediator.Results.BillResults;
using HealthCare.Operations.Application.Interfaces;
using HealthCare.Operations.Configurations;
using HealthCare.Operations.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Application.Features.Mediator.Handlers.BillHandlers
{
    public class DeleteBillCommandHandler : IRequestHandler<DeleteBillCommand>
    {

        private readonly IRepository<Bill> _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public DeleteBillCommandHandler(IRepository<Bill> repository, IMapper mapper, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task Handle(DeleteBillCommand request, CancellationToken cancellationToken)
        {
            GetBillByIdQueryResult result = await _mediator.Send(new GetBillByIdQuery(request.Id));

            if (result != null)
            {
                Bill dataFromDto = _mapper.Map<Bill>(result);

                await _repository.DeleteDataAsync(dataFromDto);
            }
        }
    }
}
