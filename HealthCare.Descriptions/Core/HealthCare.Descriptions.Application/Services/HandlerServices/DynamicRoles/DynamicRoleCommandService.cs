using HealthCare.Descriptions.Application.Common.Wrappers;
using HealthCare.Descriptions.Application.Interfaces.HandlerServices.DynamicRoles;
using HealthCare.Descriptions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Descriptions.Application.Services.HandlerServices.DynamicRoles
{
    public class DynamicRoleCommandService : IDynamicRoleCommandService
    {

        public Task<InternalServiceResponse<DynamicRole>> GetDataForUpdateAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<InternalServiceResponse<Guid>> CreateAsync(DynamicRole entity)
        {
            throw new NotImplementedException();
        }

        public Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(DynamicRole entity)
        {
            throw new NotImplementedException();
        }

        public Task<InternalServiceResponse<bool>> RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}