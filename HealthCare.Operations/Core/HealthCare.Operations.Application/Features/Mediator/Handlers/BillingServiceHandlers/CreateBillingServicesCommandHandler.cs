using AutoMapper;
using HealthCare.Operations.Application.Features.Mediator.Commands.BillingServiceCommands;
using HealthCare.Operations.Application.Features.Mediator.Handlers.Wrappers;
using HealthCare.Operations.Application.Interfaces;
using HealthCare.Operations.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog;

namespace HealthCare.Operations.Application.Features.Mediator.Handlers.BillingServiceHandlers
{
    public class CreateBillingServicesCommandHandler : IRequestHandler<CreateBillingServicesCommand, GenericHandlerResponse>
    {

        private readonly IRepository<BillingService> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateBillingServicesCommandHandler> _logger;

        public CreateBillingServicesCommandHandler(IRepository<BillingService> repository, IMapper mapper, ILogger<CreateBillingServicesCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GenericHandlerResponse> Handle(CreateBillingServicesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                List<BillingService> dataListFromDto = _mapper.Map<List<BillingService>>(request.CreateListData);

                await _repository.CreateListDataAsync(dataListFromDto);

                _logger.LogInformation("Handler: {Handler} Message: {Message}",
                    nameof(CreateBillingServicesCommandHandler),
                    HandlerResponseMessageConstant.SuccessCreatedData);

                return new GenericHandlerResponse
                {
                    HandlerStatus = true,
                    HandlerMessage = HandlerResponseMessageConstant.SuccessCreatedData,
                    TimeStamp = DateTime.UtcNow
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Handler: {Handler} Message: {Message}",
                    nameof(CreateBillingServicesCommandHandler),
                    ex.Message);

                return new GenericHandlerResponse
                {
                    HandlerStatus = true,
                    HandlerMessage = HandlerResponseMessageConstant.FailCreatedData,
                    TimeStamp = DateTime.UtcNow
                };
            }
        }
    }
}
