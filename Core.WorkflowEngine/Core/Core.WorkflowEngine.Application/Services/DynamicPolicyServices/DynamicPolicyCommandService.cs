using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.DynamicPolicyServices;
using Core.WorkflowEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Services.DynamicPolicyServices
{
    public class DynamicPolicyCommandService : IDynamicPolicyCommandService
    {
        private readonly IRepository<DynamicPolicy> _repository;

        public DynamicPolicyCommandService(IRepository<DynamicPolicy> repository)
        {
            _repository = repository;
        }

        public async Task<InternalServiceResponse<DynamicPolicy>> GetDataForUpdateAsync(Guid id)
        {
            DBQueryOptions<DynamicPolicy> options = new DBQueryOptions<DynamicPolicy>();
            options.filter = x => x.Id == id;

            DynamicPolicy repoResponse = await _repository.GetDataAsync(options);

            if (repoResponse == null)
                return InternalServiceResponse<DynamicPolicy>.Failure("Data not found!");

            return InternalServiceResponse<DynamicPolicy>.Success(repoResponse);
        }

        public async Task<InternalServiceResponse<Guid>> CreateAsync(DynamicPolicy entity, CancellationToken cancellationToken)
        {
            Guid repoResponse = await _repository.CreateDataAsync(entity);

            return InternalServiceResponse<Guid>.Success(repoResponse);
        }

        public async Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(DynamicPolicy entity)
        {
            DateTimeOffset repoResponse = await _repository.UpdateDataAsync(entity);

            return InternalServiceResponse<DateTimeOffset>.Success(repoResponse);
        }

        public async Task<InternalServiceResponse<bool>> DeleteAsync(Guid id)
        {
            InternalServiceResponse<DynamicPolicy> existedData = await GetDataForUpdateAsync(id);

            if (!existedData.IsSuccess)
                return InternalServiceResponse<bool>.Failure(existedData.ServiceMessage);

            await _repository.DeleteDataAsync(existedData.Data);

            return InternalServiceResponse<bool>.Success(true);
        }
    }
}