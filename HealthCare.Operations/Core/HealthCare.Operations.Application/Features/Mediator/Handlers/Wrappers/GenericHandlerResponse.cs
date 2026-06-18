using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Application.Features.Mediator.Handlers.Wrappers
{
    public class GenericHandlerResponse
    {
        public bool HandlerStatus { get; set; }
        public string HandlerMessage { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
