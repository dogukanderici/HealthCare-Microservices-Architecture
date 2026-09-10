using HealthCare.Descriptions.Application.Features.Wrappers.Responses;
using HealthCare.Descriptions.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.Mediators.QuotaTypes.Commands
{
    public class RemoveQuotaTypeCommand : IRequest<InternalHandlerResponse<bool>>, ITransactionalRequest
    {
        public Guid Id { get; set; }

        public RemoveQuotaTypeCommand(Guid id)
        {
            Id = id;
        }
    }
}
