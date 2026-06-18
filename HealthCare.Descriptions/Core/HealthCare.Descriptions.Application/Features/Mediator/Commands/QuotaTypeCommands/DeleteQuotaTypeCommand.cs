using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediator.Commands.QuotaTypeCommands
{
    public class DeleteQuotaTypeCommand : IRequest
    {
        public Guid Id { get; set; }

        public DeleteQuotaTypeCommand(Guid id)
        {
            Id = id;
        }
    }
}
