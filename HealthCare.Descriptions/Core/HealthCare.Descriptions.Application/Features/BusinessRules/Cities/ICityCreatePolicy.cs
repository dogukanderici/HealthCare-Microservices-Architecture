using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.BusinessRules.Cities
{
    public interface ICityCreatePolicy : IPolicyRule<City>
    {
        // ICityPolicy _cityPolicy; ile çağrılınca ExecuteAllRulesAsync() kullanmak için. Özel metotlar private olarak class'a yazılabilir.
    }
}