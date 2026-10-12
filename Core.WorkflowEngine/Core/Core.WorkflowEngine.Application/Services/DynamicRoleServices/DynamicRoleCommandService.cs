using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.DynamicRoleServices;
using Core.WorkflowEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WorkflowEngine.Application.Services.DynamicRoleServices
{
    public class DynamicRoleCommandService : IDynamicRoleCommandService
    {
        private readonly IRepository<DynamicRole> _repository;

        public DynamicRoleCommandService(IRepository<DynamicRole> repository)
        {
            _repository = repository;
        }

        public async Task<InternalServiceResponse<DynamicRole>> GetDataForUpdateAsync(Guid id)
        {
            DBQueryOptions<DynamicRole> options = new DBQueryOptions<DynamicRole>();
            options.filter = x => x.Id == id;

            DynamicRole repoResponse = await _repository.GetDataAsync(options);

            if (repoResponse == null)
                return InternalServiceResponse<DynamicRole>.Failure("Data not found!");

            return InternalServiceResponse<DynamicRole>.Success(repoResponse);
        }

        public async Task<InternalServiceResponse<Guid>> CreateAsync(DynamicRole entity, CancellationToken cancellationToken)
        {
            Guid repoResponse = await _repository.CreateDataAsync(entity);

            return InternalServiceResponse<Guid>.Success(repoResponse);
        }

        public async Task<InternalServiceResponse<DateTimeOffset>> UpdateAsync(DynamicRole entity)
        {
            DateTimeOffset repoResponse = await _repository.UpdateDataAsync(entity);

            return InternalServiceResponse<DateTimeOffset>.Success(repoResponse);
        }

        public async Task<InternalServiceResponse<bool>> DeleteAsync(Guid id)
        {
            InternalServiceResponse<DynamicRole> existedData = await GetDataForUpdateAsync(id);

            if (!existedData.IsSuccess)
                return InternalServiceResponse<bool>.Failure(existedData.ServiceMessage);

            await _repository.DeleteDataAsync(existedData.Data);

            return InternalServiceResponse<bool>.Success(true);
        }
    }
}