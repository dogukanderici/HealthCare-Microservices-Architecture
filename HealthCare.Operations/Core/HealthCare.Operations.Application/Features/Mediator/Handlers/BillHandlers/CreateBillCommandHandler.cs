using AutoMapper;
using HealthCare.Operations.Application.Features.Mediator.Commands.BillCommands;
using HealthCare.Operations.Application.Features.Mediator.Handlers.Wrappers;
using HealthCare.Operations.Application.Interfaces;
using HealthCare.Operations.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog;

namespace HealthCare.Operations.Application.Features.Mediator.Handlers.BillHandlers
{
    public class CreateBillCommandHandler : IRequestHandler<CreateBillCommand, GenericHandlerResponse>
    {
        private readonly IRepository<Bill> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateBillCommandHandler> _logger;

        public CreateBillCommandHandler(IRepository<Bill> repository, IMapper mapper, ILogger<CreateBillCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GenericHandlerResponse> Handle(CreateBillCommand request, CancellationToken cancellationToken)
        {
            try
            {
                request.Id = Guid.NewGuid();
                Bill dataFromDto = _mapper.Map<Bill>(request);

                await _repository.CreateDataAsync(dataFromDto);

                _logger.LogInformation("Handler: {Handler} Message: {Message}",
                    nameof(CreateBillCommandHandler),
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
                    nameof(CreateBillCommandHandler),
                   ex.Message);

                return new GenericHandlerResponse
                {
                    HandlerStatus = false,
                    HandlerMessage = HandlerResponseMessageConstant.FailCreatedData,
                    TimeStamp = DateTime.UtcNow
                };
            }
        }
    }
}
