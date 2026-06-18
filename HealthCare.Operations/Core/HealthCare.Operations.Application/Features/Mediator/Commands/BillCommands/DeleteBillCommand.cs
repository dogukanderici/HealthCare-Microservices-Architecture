using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Application.Features.Mediator.Commands.BillCommands
{
    public class DeleteBillCommand : IRequest
    {
        public Guid Id { get; set; }

        public DeleteBillCommand(Guid id)
        {
            Id = id;
        }
    }
}
