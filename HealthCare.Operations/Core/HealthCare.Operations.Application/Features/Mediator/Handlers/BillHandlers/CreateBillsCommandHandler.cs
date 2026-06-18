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
    public class CreateBillsCommandHandler : IRequestHandler<CreateBillsCommand>
    {
        private readonly IRepository<Bill> _repository;
        private readonly IMapper _mapper;

        public CreateBillsCommandHandler(IRepository<Bill> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task Handle(CreateBillsCommand request, CancellationToken cancellationToken)
        {
            foreach (var item in request.CreateListData)
            {
                item.Id = Guid.NewGuid();
            }

            List<Bill> dataListFromDto = _mapper.Map<List<Bill>>(request.CreateListData);

            await _repository.CreateListDataAsync(dataListFromDto);
        }
    }
}
