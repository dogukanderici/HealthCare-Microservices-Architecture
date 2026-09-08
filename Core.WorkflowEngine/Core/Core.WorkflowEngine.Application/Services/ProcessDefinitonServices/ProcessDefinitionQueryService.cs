using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessDefitinionsServices;
using Core.WorkflowEngine.Application.ServiceDtos.ProcessDefinitionDtos;
using Core.WorkflowEngine.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Core.WorkflowEngine.Application.Services.ProcessDefiniitonServices
{
    public class ProcessDefinitionQueryService : IProcessDefinitionQueryService
    {
        private readonly IRepository<ProcessDefinition> _repository;
        private readonly ILogger<ProcessDefinitionQueryService> _logger;

        public ProcessDefinitionQueryService(IRepository<ProcessDefinition> respository, ILogger<ProcessDefinitionQueryService> logger)
        {
            _repository = respository;
            _logger = logger;
        }

        public async Task<InternalServiceResponse<IReadOnlyCollection<ProcessDefinition>>> GetDatasAsync(ProcessDefinitionFilterDto? filterDto = null)
        {
            DBQueryOptions<ProcessDefinition> dBQueryOptions = new DBQueryOptions<ProcessDefinition>();

            IReadOnlyCollection<ProcessDefinition> result = await _repository.GetAllDataAsync(dBQueryOptions);

            return InternalServiceResponse<IReadOnlyCollection<ProcessDefinition>>.Success(result);
        }

        public async Task<InternalServiceResponse<ProcessDefinition>> GetDataByIdAsync(Guid id)
        {
            DBQueryOptions<ProcessDefinition> dbQueryOptions = new DBQueryOptions<ProcessDefinition>();

            Expression<Func<ProcessDefinition, bool>> filter = x => x.Id == id;
            dbQueryOptions.filter = filter;

            ProcessDefinition result = await _repository.GetDataAsync(dbQueryOptions);

            return InternalServiceResponse<ProcessDefinition>.Success(result);
        }

        public async Task<InternalServiceResponse<IReadOnlyCollection<ProcessDefinition>>> GetDatasByFilterAsync(ProcessDefinitionFilterDto filterDto)
        {
            DBQueryOptions<ProcessDefinition> dBQueryOptions = GetDbQueryOptions(filterDto);

            IReadOnlyCollection<ProcessDefinition> result = await _repository.GetAllDataAsync(dBQueryOptions);

            return InternalServiceResponse<IReadOnlyCollection<ProcessDefinition>>.Success(result);
        }

        public async Task<InternalServiceResponse<ProcessDefinition>> GetDataForLastestVersionAsync(Guid processSpecId)
        {
            DBQueryOptions<ProcessDefinition> dBQueryOptions = new DBQueryOptions<ProcessDefinition>();

            Expression<Func<ProcessDefinition, bool>> filter = x => (
                x.ProcessSpecId == processSpecId &&
                x.IsActive == true
            );

            Expression<Func<ProcessDefinition, object>> orderBy = x => x.VersionNumber;

            int sortingType = 1; // Descending
            int dataTakeNumber = 1; // Son versiyonu alır.

            dBQueryOptions.filter = filter;
            dBQueryOptions.sortingType = sortingType;
            dBQueryOptions.orderBy = orderBy;
            dBQueryOptions.DataTakeNumber = dataTakeNumber;

            ProcessDefinition result = await _repository.GetDataAsync(dBQueryOptions);

            return InternalServiceResponse<ProcessDefinition>.Success(result);
        }

        public async Task<InternalServiceResponse<int>> GetDataCount(ProcessDefinitionFilterDto filterDto)
        {
            DBQueryOptions<ProcessDefinition> dBQueryOptions = GetDbQueryOptions(filterDto, false);

            int result = await _repository.GetAllDataCountAsync(dBQueryOptions);

            return InternalServiceResponse<int>.Success(result);
        }

        private DBQueryOptions<ProcessDefinition> GetDbQueryOptions(ProcessDefinitionFilterDto filterDto, bool isRelationalQuery = true)
        {
            DBQueryOptions<ProcessDefinition> dBQueryOptions = new DBQueryOptions<ProcessDefinition>();

            Expression<Func<ProcessDefinition, bool>> filter = x => (
            (!filterDto.ProcessSpecId.HasValue || x.ProcessSpecId == filterDto.ProcessSpecId) &&
            (!filterDto.IsActive.HasValue || x.IsActive == filterDto.IsActive) &&
            (string.IsNullOrEmpty(filterDto.ProcessName) || x.ProcessName == filterDto.ProcessName)
            );

            dBQueryOptions.filter = filter;

            return dBQueryOptions;
        }
    }
}