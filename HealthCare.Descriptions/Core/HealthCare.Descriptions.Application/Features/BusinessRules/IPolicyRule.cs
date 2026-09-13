using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Responses;
using HealthCare.Descriptions.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.BusinessRules
{
    public interface IPolicyRule<T>
        where T : class, IEntity
    {
        Task<InternalPolicyResponse> ExecuteAllRulesAsync(T entity); // Tüm kural metotları burada çalıştırılır ve hepsinin true dönmesi beklenir. Aski halde işlem devam etmez.
    }
}