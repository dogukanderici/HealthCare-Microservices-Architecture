using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Interfaces
{
    public interface ICurrentUserService
    {
        public Guid UserId { get; }
        public DateTimeOffset CurrentDate { get; }
    }
}
