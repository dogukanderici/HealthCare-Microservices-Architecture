using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Application.Features.Mediator.Commands.BillingServiceCommands
{
    public class DeleteBillingServiceCommand : IRequest
    {
        public Guid Id { get; set; }

        public DeleteBillingServiceCommand(Guid id)
        {
            Id = id;
        }
    }
}
