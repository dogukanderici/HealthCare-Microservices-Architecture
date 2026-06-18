using AutoMapper;
using HealthCare.Operations.Application.Features.Mediator.Commands.BillCommands;
using HealthCare.Operations.Application.Features.Mediator.Handlers.Wrappers;
using HealthCare.Operations.Application.Interfaces;
using HealthCare.Operations.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Operations.Application.Features.Mediator.Handlers.BillHandlers
{
    public class CreateBillWithBillingServiceCommandHandler : IRequestHandler<CreateBillWithBillingServiceCommand, GenericHandlerResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateBillWithBillingServiceCommandHandler> _logger;

        public CreateBillWithBillingServiceCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<CreateBillWithBillingServiceCommandHandler> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<GenericHandlerResponse> Handle(CreateBillWithBillingServiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                request.Id = Guid.NewGuid();

                Bill dataFromBillDto = _mapper.Map<Bill>(request);
                _unitOfWork.BillRepository.CreateData(dataFromBillDto);

                foreach (var item in request.BillingServicesData)
                {
                    item.BillingId = request.Id;
                }

                List<BillingService> dataFromBillingServiceDto = _mapper.Map<List<BillingService>>(request.BillingServicesData);
                _unitOfWork.BillingServiceRepository.CreateListData(dataFromBillingServiceDto);

                await _unitOfWork.CommitAsync();

                _logger.LogInformation("Handler: {Handler} Message:{Message}",
                    nameof(CreateBillWithBillingServiceCommandHandler),
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
                _logger.LogError("Handler: {Handler} Message:{Message}",
                    nameof(CreateBillWithBillingServiceCommandHandler),
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
