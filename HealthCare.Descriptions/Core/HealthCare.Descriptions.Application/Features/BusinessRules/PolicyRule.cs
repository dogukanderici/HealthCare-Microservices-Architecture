using HealthCare.Descriptions.Application.Common.Parameters;
using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Features.BusinessRules.Commons.Responses;
using HealthCare.Descriptions.Application.Interfaces;
using HealthCare.Descriptions.Domain.Abstracts;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Features.BusinessRules
{
    public abstract class PolicyRule<T> : IPolicyRule<T>
        where T : class, IEntity

    {
        // protected: Sadece bu sınıftan miras alanlar görebilir (Service göremez).
        // abstract: Miras alan her sınıf kendi tipine göre bu metodu yazmak zorundadır.
        protected abstract Task<InternalPolicyResponse> CountExistingDataAsync(T entity); // Id dışındaki diğer property'ler üzerinden de tekillik kontrolü yapılabilmesi için.

        public abstract Task<InternalPolicyResponse> ExecuteAllRulesAsync(T entity);
    }
}
