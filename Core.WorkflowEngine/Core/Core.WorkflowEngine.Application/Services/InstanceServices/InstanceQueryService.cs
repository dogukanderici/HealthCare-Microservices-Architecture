using AutoMapper;
using Core.WorkflowEngine.Application.Commons.Parameters;
using Core.WorkflowEngine.Application.Commons.Wrappers;
using Core.WorkflowEngine.Application.Interfaces;
using Core.WorkflowEngine.Application.Interfaces.HandlerServices.InstanceServices;
using Core.WorkflowEngine.Domain.Entities;

namespace Core.WorkflowEngine.Application.Services.InstanceServices
{
    public class InstanceQueryService : IInstanceQueryService
    {
        private readonly IRepository<Instance> _repository;
        private readonly IMapper _mapper;

        public InstanceQueryService(IRepository<Instance> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<InternalServiceResponse<IReadOnlyCollection<TResult>>> GetDatasAsync<TResult>(DBQueryOptions<Instance>? options = null)
        {
            IReadOnlyCollection<Instance> existedDataList = await _repository.GetAllDataAsync(options);

            return InternalServiceResponse<IReadOnlyCollection<TResult>>.Success(_mapper.Map<IReadOnlyCollection<TResult>>(existedDataList));
        }

        public async Task<InternalServiceResponse<TResult>> GetDataByIdAsync<TResult>(Guid id)
        {
            DBQueryOptions<Instance> dBQueryOptions = new DBQueryOptions<Instance>();
            dBQueryOptions.filter = x => x.Id == id;

            Instance existedData = await _repository.GetDataAsync(dBQueryOptions);

            return InternalServiceResponse<TResult>.Success(_mapper.Map<TResult>(existedData));
        }

        public async Task<InternalServiceResponse<int>> GetDataCount(DBQueryOptions<Instance> options)
        {
            int existedCount = await _repository.GetAllDataCountAsync(options);

            return InternalServiceResponse<int>.Success(existedCount);
        }

        public async Task<InternalServiceResponse<IReadOnlyCollection<TResult>>> GetDatasByFilterAsync<TResult>(DBQueryOptions<Instance> options)
        {
            IReadOnlyCollection<Instance> existedDataList = await _repository.GetAllDataAsync(options);

            return InternalServiceResponse<IReadOnlyCollection<TResult>>.Success(_mapper.Map<IReadOnlyCollection<TResult>>(existedDataList));
        }
    }
}