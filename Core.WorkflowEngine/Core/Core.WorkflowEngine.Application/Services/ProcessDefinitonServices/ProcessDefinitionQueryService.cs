using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.ProcessDefitinionsServices;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Services.ProcessDefiniitonServices
{
    public class ProcessDefinitionQueryService : IProcessDefinitionQueryService
    {
        private readonly IRepository<ProcessDefinition> _repository;
        private readonly IMapper _mapper;

        public ProcessDefinitionQueryService(IRepository<ProcessDefinition> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<InternalServiceResponse<IReadOnlyCollection<TResult>>> GetDatasAsync<TResult>(DBQueryOptions<ProcessDefinition>? options = null)
        {
            IReadOnlyCollection<ProcessDefinition> result = await _repository.GetAllDataAsync(options);

            return InternalServiceResponse<IReadOnlyCollection<TResult>>.Success(_mapper.Map<IReadOnlyCollection<TResult>>(result));
        }

        public async Task<InternalServiceResponse<TResult>> GetDataByIdAsync<TResult>(Guid id)
        {
            DBQueryOptions<ProcessDefinition> dbQueryOptions = new DBQueryOptions<ProcessDefinition>();
            dbQueryOptions.filter = x => x.Id == id;

            ProcessDefinition result = await _repository.GetDataAsync(dbQueryOptions);

            return InternalServiceResponse<TResult>.Success(_mapper.Map<TResult>(result));
        }

        public async Task<InternalServiceResponse<IReadOnlyCollection<TResult>>> GetDatasByFilterAsync<TResult>(DBQueryOptions<ProcessDefinition> options)
        {
            IReadOnlyCollection<ProcessDefinition> result = await _repository.GetAllDataAsync(options);

            return InternalServiceResponse<IReadOnlyCollection<TResult>>.Success(_mapper.Map<IReadOnlyCollection<TResult>>(result));
        }

        public async Task<InternalServiceResponse<TResult>> GetDataForLastestVersionAsync<TResult>(Guid processSpecId)
        {
            int sortingType = 1; // Descending
            int dataTakeNumber = 1; // Son versiyonu alır.

            DBQueryOptions<ProcessDefinition> dBQueryOptions = new DBQueryOptions<ProcessDefinition>();

            dBQueryOptions.filter = x => (
                x.ProcessSpecId == processSpecId &&
                x.IsActive == true
            );
            dBQueryOptions.orderBy = x => x.VersionNumber;
            dBQueryOptions.sortingType = sortingType;
            dBQueryOptions.DataTakeNumber = dataTakeNumber;

            ProcessDefinition result = await _repository.GetDataAsync(dBQueryOptions);

            return InternalServiceResponse<TResult>.Success(_mapper.Map<TResult>(result));
        }

        public async Task<InternalServiceResponse<int>> GetDataCount(DBQueryOptions<ProcessDefinition> options)
        {
            int result = await _repository.GetAllDataCountAsync(options);

            return InternalServiceResponse<int>.Success(result);
        }
    }
}