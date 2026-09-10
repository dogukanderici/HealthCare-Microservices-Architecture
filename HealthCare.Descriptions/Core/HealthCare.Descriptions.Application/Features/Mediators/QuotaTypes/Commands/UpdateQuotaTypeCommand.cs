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
    public class UpdateQuotaTypeCommand : IRequest<InternalHandlerResponse<DateTimeOffset>>, ITransactionalRequest
    {
        public Guid Id { get; set; }
        public string QuotaTypeCode { get; set; }
        public string QuotaTypeName { get; set; }
        public bool IsAvailable { get; set; }
    }
}
