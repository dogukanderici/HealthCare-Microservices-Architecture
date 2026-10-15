using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.DynamicPolicies;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Services.HandlerServices.DynamicPolicies
{
    public class DynamicPolicyCommandService : IDynamicPolicyCommandService
    {

        public Task<InternalServiceResponse<DynamicPolicy>> GetDataForUpdateAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<InternalServiceResponse<Guid>> CreateAsync(DynamicPolicy entity)
        {
            throw new NotImplementedException();
        }

        public Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(DynamicPolicy entity)
        {
            throw new NotImplementedException();
        }

        public Task<InternalServiceResponse<bool>> RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
